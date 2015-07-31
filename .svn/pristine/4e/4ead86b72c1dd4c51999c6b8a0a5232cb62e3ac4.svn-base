import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.StringTokenizer;

import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.conf.Configured;
import org.apache.hadoop.filecache.DistributedCache;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.LongWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapred.FileInputFormat;
import org.apache.hadoop.mapred.FileOutputFormat;
import org.apache.hadoop.mapred.JobClient;
import org.apache.hadoop.mapred.JobConf;
import org.apache.hadoop.mapred.MapReduceBase;
import org.apache.hadoop.mapred.Mapper;
import org.apache.hadoop.mapred.OutputCollector;
import org.apache.hadoop.mapred.Reducer;
import org.apache.hadoop.mapred.Reporter;
import org.apache.hadoop.mapred.TextInputFormat;
import org.apache.hadoop.mapred.TextOutputFormat;
import org.apache.hadoop.util.StringUtils;
import org.apache.hadoop.util.Tool;
import org.apache.hadoop.util.ToolRunner;

public class WordCount extends Configured implements Tool {

	public static class Map extends MapReduceBase implements
			Mapper<LongWritable, Text, Text, IntWritable> {

		private static enum Counters {
			INPUT_WORDS
		};

		private final static IntWritable one = new IntWritable(1);
		private final Text word = new Text();
		private boolean caseSensitive = true;
		private final Set<String> patternsToSkip = new HashSet<String>();
		private long numRecords = 0;
		private String inputFile;

		@Override
		public void configure(JobConf job) {
			caseSensitive = job.getBoolean("wordcount.case.sensitive", true);
			inputFile = job.get("map.inpu.file");
			if (job.getBoolean("wordcount.skip.patters", false)) {
				Path[] patternFiles = new Path[0];
				try {
					patternFiles = DistributedCache.getLocalCacheFiles(job);
				} catch (IOException ioe) {
					System.err
							.println("Caught exception while getting cached files: "
									+ StringUtils.stringifyException(ioe));
				}
				for (Path patternFile : patternFiles) {
					parseSkipFile(patternFile);
				}
			}
		}

		private void parseSkipFile(Path patternFile) {
			try {
				BufferedReader fis = new BufferedReader(new FileReader(
						patternFile.toString()));
				String pattern = null;
				while ((pattern = fis.readLine()) != null) {
					patternsToSkip.add(pattern);
				}
				fis.close();
			} catch (IOException ioe) {
				System.err
						.println("Caught exception while parsing the cached file '"
								+ patternFile
								+ "' : "
								+ StringUtils.stringifyException(ioe));
			}
		}

		public void map(LongWritable key, Text value,
				OutputCollector<Text, IntWritable> output,
				org.apache.hadoop.mapred.Reporter reporter) throws IOException {
			String line = (caseSensitive) ? value.toString() : value.toString()
					.toLowerCase();
			for (String pattern : patternsToSkip) {
				line = line.replaceAll(inputFile, "");
			}

			StringTokenizer tokenizer = new StringTokenizer(line);
			while (tokenizer.hasMoreTokens()) {
				word.set(tokenizer.nextToken());
				output.collect(word, one);
				reporter.incrCounter(Counters.INPUT_WORDS, 1);
			}

			if ((++numRecords % 100) == 0) {
				reporter.setStatus("Finished processing " + numRecords
						+ " records " + "from the input file: " + inputFile);
			}
		}
	}

	public static class Reduce extends MapReduceBase implements
			Reducer<Text, IntWritable, Text, IntWritable> {

		public void reduce(Text key, Iterator<IntWritable> values,
				OutputCollector<Text, IntWritable> output, Reporter reporter)
				throws IOException {
			int sum = 0;
			while (values.hasNext()) {
				sum += values.next().get();
			}
			output.collect(key, new IntWritable(sum));
		}
	}

	public int run(String[] args) throws Exception {
		JobConf conf = new JobConf(getConf(), WordCount.class);
		conf.setJobName("wordcount");

		conf.setOutputKeyClass(Text.class);
		conf.setOutputValueClass(IntWritable.class);

		conf.setMapperClass(Map.class);
		conf.setCombinerClass(Reduce.class);
		conf.setReducerClass(Reduce.class);

		conf.setInputFormat(TextInputFormat.class);
		conf.setOutputFormat(TextOutputFormat.class);

		List<String> other_args = new ArrayList<String>();
		for (int i = 0; i < args.length; i++) {
			if ("-skip".equals(args[i])) {
				DistributedCache
						.addCacheFile(new Path(args[++i]).toUri(), conf);
				conf.setBoolean("wordcount.skip.patterns", true);
			} else {
				other_args.add(args[i]);
			}
		}

		FileInputFormat.setInputPaths(conf, new Path(other_args.get(0)));
		FileOutputFormat.setOutputPath(conf, new Path(other_args.get(1)));
		JobClient.runJob(conf);

		return 0;
	}

	public static void main(String[] args) throws Exception {
		int ret = ToolRunner.run(new Configuration(), new WordCount(), args);
		System.exit(ret);
	}
}
