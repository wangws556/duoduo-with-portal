namespace YoYoStudio.RoomService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.wcfServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			this.wcfServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			// 
			// wcfServiceInstaller
			// 
			this.wcfServiceInstaller.ServiceName = "9258 Room Service";
			this.wcfServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// wcfServiceProcessInstaller
			// 
			this.wcfServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.wcfServiceProcessInstaller.Password = null;
			this.wcfServiceProcessInstaller.Username = null;
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.wcfServiceInstaller,
            this.wcfServiceProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceInstaller wcfServiceInstaller;
        private System.ServiceProcess.ServiceProcessInstaller wcfServiceProcessInstaller;

    }
}