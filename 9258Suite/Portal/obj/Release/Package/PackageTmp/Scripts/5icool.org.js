(function (window) {

    if (!window.yuuk) {
        window.yuuk = {}
    }
    yuuk.using = function () {
        var a = arguments,
        o = this,
        i = 0,
        j, d, arg, isExist;
        arg = a[0],
        isExist = a[1];
        if (arg && arg.indexOf(".")) {
            d = arg.split(".");
            for (j = (d[0] == "yuuk") ? 1 : 0; j < d.length; j++) {
                if (!o[d[j]] && isExist) {
                    return null
                }
                o[d[j]] = o[d[j]] || {};
                o = o[d[j]]
            }
        } else {
            o[arg] = o[arg] || {}
        }
        return o
    };

})(window);
(function (a) {
    $.extend(a, {
        lazyloadImg: function (g, i) {
            var f = {
                threshold: 0,
                failurelimit: 0,
                event: "scroll",
                effect: "show",
                loadingEffect: false,
                container: window
            };

            var c = $(g);
            var b = [];
            c.filter("img").each(function () {
                var k = this;
                k.loaded = false;
                var m = $(k);
                if (m.attr("data-src") == undefined) {
                    m.attr("data-src", m.attr("src"));
                    m.removeAttr("src")
                }
                if (f.loadingEffect) {
                    var l = "./images/blank.png";
                    if (m.attr("src") != l) {
                        m.attr("src", l);
                        m.addClass("loading")
                    }
                }
            });

        }

    })
})(yuuk.using("Utils"));

(function (a) {
    $.extend(a, {
        timer: function (g, f) {
            var d = {
                name: "TIMER#0000",
                interval: 1000,
                immediately: true
            };
            if (f) {
                $.extend(d, f)
            }
            if (typeof g !== "function") {
                throw new Error("您没有为Timer指定有效的执行函数。")
            }
            var e = null;
            var c = 0;
            var b = {
                start: function () {
                    if (e != null) {
                        clearInterval(e);
                        e = null
                    }
                    var h = function () {
                        c += 1;
                        g(b)
                    };
                    e = setInterval(h, d.interval);
                    if (d.immediately) {
                        h()
                    }
                },
                stop: function () {
                    if (e != null) {
                        clearInterval(e);
                        e = null
                    }
                },
                getCounter: function () {
                    return c
                }
            };
            return b
        },
        gotoAnchor: yuuk.using("Utils").gotoAnchor
    })
})(yuuk.using("Common"));

(function (a) {
    a.Slider = function (g, f) {
        var d = {
            stepSize: 1,
            clipSize: 1,
            sliderFinder: ".j-silder",
            sliderItemFinder: ".j-item",
            sliderItemWidth: 0,
            sliderItemCount: 0,
            mode: "default",
            goMode: "left",
            onInit: function (s, r) { },
            onSlidBegin: function (s, r) { },
            onSlidEnd: function (s, r) { }
        };
        if (f) {
            $.extend(d, f)
        }
        var i = $(g);
        var m = $(d.sliderFinder, i);
        var q = $(d.sliderItemFinder, i);
        var b = d.sliderItemCount > 0 ? d.sliderItemCount : q.size();
        var k = d.sliderItemWidth > 0 ? d.sliderItemWidth : q.outerWidth(true);
        var e = false;
        var l = false;
        var j = 0;
        var c = function (r) {
            switch (d.goMode) {
                case "left":
                    return {
                        left:
                        r * k * -1
                    };
                case "top":
                    return {
                        top:
                        r * k * -1
                    };
                case "marginLeft":
                    return {
                        marginLeft:
                        r * k * -1
                    };
                case "marginTop":
                    return {
                        marginTop:
                        r * k * -1
                    };
                default:
                    return {
                        left:
                        r * k * -1
                    }
            }
        };
        if (b > d.clipSize) {
            if (d.mode === "round") {
                m.append(q.clone(true));
                m.append(q.clone(true));
                e = true;
                l = true;
                q = $(d.sliderItemFinder, i)
            } else {
                var h = 0;
                var n = b - d.clipSize;
                if (j >= n) {
                    j = n;
                    l = false
                } else {
                    l = true
                }
                if (j <= h) {
                    j = 0;
                    e = false
                } else {
                    e = true
                }
            }
        } else {
            e = false;
            l = false
        }
        m.find("img[data-src]").each(function () {
            var r = this;
            r.loaded = false;
            var s = $(r).attr("src", "./images/blank.png").addClass("loading").one("do-load",
            function () {
                if (r.loaded) {
                    return
                }
                $("<img />").bind("load",
                function () {
                    s.attr("src", s.attr("data-src"));
                    r.loaded = true
                }).attr("src", s.attr("data-src"))
            })
        });
        var o = null;
        var p = {
            gotoIndex: function (r) {
                d.onSlidBegin(p, {
                    index: r
                });
                m.animate(c(r), {
                    queue: false,
                    complete: function () {
                        j = r;
                        clearTimeout(o);
                        o = setTimeout(function () {
                            d.onSlidEnd(p, {
                                index: r
                            })
                        },
                        100)
                    }
                });
                for (var s = r; s < r + d.stepSize; s++) {
                    q.eq(s).find("img[data-src]").trigger("do-load")
                }
            },
            gotoNextStep: function () {
                var s = j + d.stepSize;
                if (d.mode === "round") {
                    var r = b * 3 - d.clipSize;
                    if (s > r) {
                        j = j - b;
                        m.css(c(j));
                        s = j + d.stepSize
                    }
                } else {
                    var r = b - d.clipSize;
                    if (s >= r) {
                        s = r;
                        l = false
                    } else {
                        l = true
                    }
                    e = true
                }
                this.gotoIndex(s);
                return l
            },
            gotoPreStep: function () {
                var r = j - d.stepSize;
                if (d.mode === "round") {
                    if (r < 0) {
                        j = j + b;
                        m.css(c(j));
                        r = j - d.stepSize
                    }
                } else {
                    if (r <= 0) {
                        r = 0;
                        e = false
                    } else {
                        e = true
                    }
                    l = true
                }
                this.gotoIndex(r);
                return e
            },
            isPreEnable: function () {
                return e
            },
            isNextEnable: function () {
                return l
            }
        };
        p.gotoIndex(0);
        d.onInit(p, {});
        return p
    };
    a.slider = a.Slider
})(yuuk.using("UI"));






(function (c) {
    var d = yuuk.using("UI");
    var a = yuuk.using("Common");
    (function (g, e, h) {
        function f(i, k) {
            var j = this;
            if (!g(i)[0]) {
                throw "Path Container Error"
            }
            if (!(j instanceof f)) {
                return new f(i, k)
            }
            g.extend(j.options = {},
            f.Options, k);
            j.container = g(i);
            j._init()
        }
        g.extend(f, {
            Options: {
                size: [110, 124],
                xy: [10, 226],
                zIndex: 100,
                menuSize: [65, 65],
                itemSize: [30, 30],
                modCls: "path",
                itemCls: "path-item",
                duration: 100,
                rate: 100
            }
        });
        g.extend(f.prototype, {
            _init: function () {
                var j = this,
                i = j.container,
                l = j.options,
                m = l.itemCls,
                p = l.zIndex,
                o = l.itemSize,
                k = l.menuSize,
                n = j.xy = {
                    menu: l.button[0][0]
                };
                i.hide().addClass(l.modCls).css({
                    position: "relative",
                    zIndex: p,
                    width: l.size[0],
                    height: l.size[1],
                    left: l.xy[0],
                    top: l.xy[1]
                }).show();
                n.button = [(k[0] - o[0]) / 2 + n.menu[0], (k[1] - o[1]) / 2 + n.menu[1]];
                j.buttons = [];
                g.each(l.button,
                function (r, q) {
                    var t = r === 0,
                    s = g('<a hideFocus="true"></a>').appendTo(i).attr({
                        "class": m + " " + q[1],
                        title: q[2],
                        href: q[3],
                        target: q[4] || "_blank",
                        hideFocus: "true"
                    }).css({
                        cursor: "pointer",
                        position: "absolute",
                        left: (t ? n.menu : n.button)[0],
                        top: (t ? n.menu : n.button)[1],
                        width: t ? k[0] : o[0],
                        height: t ? k[1] : o[1],
                        outline: "none",
                        zIndex: p + (t ? 2 : 1)
                    });
                    if (r === 0) {
                        j.menu = s
                    } else {
                        j.buttons.push(s)
                    }
                });
                j._attach()
            },
            _attach: function () {
                var k = this,
                l = k.options,
                o, i, n, m, j;
                if (k._task) {
                    return k
                }
                m = k.xy.menu;
                j = k.xy.button;
                i = l.duration;
                n = k.buttons.length * i;
                k._task = function (p) {
                    g(k.buttons).each(function (r, q) {
                        p = p === true;
                        var s = l.button[r + 1][0];
                        g(q).stop().animate({
                            left: p ? m[0] + s[0] : j[0],
                            top: p ? m[1] + s[1] : j[1]
                        },
                        i + (p ? l.rate * r : (n - l.rate * r)))
                    })
                };
                k.menu.mouseenter(function (p) {
                    clearTimeout(o);
                    k._task(true)
                });
                k.container.mouseleave(function (p) {
                    clearTimeout(o);
                    o = setTimeout(k._task, 100)
                });
                return k
            },
            notify: function (k, i) {
                var j;
                if (k === "menu" && i && g(i)[0] && (j = {})) {
                    g.each(["href", "target"],
                    function (l, m) {
                        j[m] = g(i).attr(m)
                    });
                    g(this.menu).attr(j)
                }
                return this
            }
        });
        e.using("UI").Path = f
    })(jQuery, yuuk);
    (function b() {
        //var e = d.Path($("<div></div>").appendTo("#j-focusPic").hide(), {
        //    button: [[[0, 43], "i-menu", "播放", "#"], [[20, -41], "i-ipad", "ipad版", "http://www.5icool.org"], [[60, -23], "i-iphone", "iphone版", "http://www.5icool.org"], [[76, 14], "i-tv", "TV版", "http://www.5icool.org"], [[68, 51], "i-pc", "乐视网络电视", "http://www.5icool.org"]]
        //});
        var j = d.slider("#j-focusPic", {
            sliderFinder: ".j-slider",
            sliderItemFinder: ".j-item",
            sliderItemWidth: 695,
            sliderItemCount: 10,
            stepSize: 1,
            clipSize: 1,
            mode: "round",
            onSlidBegin: function (o, n) {
                $("#j-focusPic .j-info").hide();
                $("#j-focusPic .j-infobg").css("opacity", 0)
            },
            onSlidEnd: function (p, n) {
                var o = [];
                o.push("<div class='infotxt w'>");
                o.push($("#j-focusPic .j-infocontainer>.infotxt").eq(n.index % 10).html());
                o.push("</div>");
                $("#j-focusPic .j-info").html(o.join("")).show();
                $("#j-focusPic .j-infobg").animate({
                    opacity: 0.5
                })
            }
        });
        var h = d.slider("#j-focusBtns", {
            sliderFinder: ".j-slider",
            sliderItemFinder: ".j-item",
            sliderItemWidth: 70,
            sliderItemCount: 10,
            clipSize: 5,
            stepSize: 5,
            onSlidBegin: function (o, n) {
                $("#j-focusBtns .pre").addClass("on-1");
                $("#j-focusBtns .next").addClass("on-2")
            },
            onSlidEnd: function (o, n) {
                if (o.isPreEnable()) {
                    $("#j-focusBtns .pre").removeClass("on-1")
                } else {
                    $("#j-focusBtns .pre").addClass("on-1")
                }
                if (o.isNextEnable()) {
                    $("#j-focusBtns .next").removeClass("on-2")
                } else {
                    $("#j-focusBtns .next").addClass("on-2")
                }
            }
        });
        var g = $("#j-focusBtns .j-item");
        var i = $("#j-focusBtns .pre , #j-focusBtns .next");
        var l = 0;
        var k = g.eq(l);
        k.siblings().removeClass("on");
        k.addClass("on");
        var m = a.timer(function () {
            l += 1;
            l = l % 10;
            var n = g.eq(l);
            n.siblings().removeClass("on");
            n.addClass("on");
            j.gotoNextStep();
            //e.notify("menu", n);
            if (l == 0) {
                h.gotoPreStep()
            }
            if (l == 5) {
                h.gotoNextStep()
            }
        },
        {
            interval: 5000,
            immediately: false
        });
        //e.notify("menu", g.eq(l));
        m.start();
        $(".Aflash").mouseover(function (n) {
            m.stop()
        }).mouseout(function (n) {
            m.start()
        });
        g.each(function (o, n) {
            var p = $(n);
            p.mouseover(function (q) {
                if (o == l) {
                    return
                }
                l = o;
                j.gotoIndex(l);
                p.siblings().removeClass("on");
                p.addClass("on");
                //e.notify("menu", g.eq(l))
            })
        });
        var f = null;
        i.mouseover(function (o) {
            var n = $(this);
            if (n.hasClass("on-1") || n.hasClass("on-2")) {
                return
            }
            clearTimeout(f);
            var p = $(this).find("i");
            f = setTimeout(function () {
                p.fadeIn("fast")
            },
            200)
        }).mouseout(function (n) {
            clearTimeout(f);
            var o = $(this).find("i");
            f = setTimeout(function () {
                o.fadeOut("fast")
            },
            200)
        }).click(function (o) {
            var n = $(this);
            if (n.hasClass("on-1") || n.hasClass("on-2")) {
                return
            }
            if ($(this).hasClass("pre")) {
                h.gotoPreStep()
            } else {
                h.gotoNextStep()
            }
        })
    })();
    d.slider("#j-hitshow", {
        sliderFinder: ".j-slider",
        sliderItemFinder: ".w128",
        sliderItemWidth: 137,
        sliderItemCount: 14,
        stepSize: 7,
        clipSize: 7,
        onInit: function (f, e) {
            $("#j-hitshow .a1").click(function (g) {
                if (!$(this).hasClass("on-1")) {
                    return
                }
                f.gotoPreStep()
            });
            $("#j-hitshow .a2").click(function (g) {
                if (!$(this).hasClass("on-2")) {
                    return
                }
                f.gotoNextStep()
            })
        },
        onSlidBegin: function () {
            $("#j-hitshow .a1").removeClass("on-1");
            $("#j-hitshow .a2").removeClass("on-2")
        },
        onSlidEnd: function (f, e) {
            if (f.isPreEnable()) {
                $("#j-hitshow .a1").addClass("on-1")
            } else {
                $("#j-hitshow .a1").removeClass("on-1")
            }
            if (f.isNextEnable()) {
                $("#j-hitshow .a2").addClass("on-2")
            } else {
                $("#j-hitshow .a2").removeClass("on-2")
            }
        }
    });
})
(yuuk.using("Plugin")); /* 酷站代码整理 http://www.5icool.org */