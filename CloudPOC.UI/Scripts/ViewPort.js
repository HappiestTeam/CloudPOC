//Ext.create('Ext.window.Window', {
//    title: 'Hello',
//    height: 200,
//    width: 400,
//    layout: 'fit',
//    items: {  // Let's put an empty grid in just to illustrate fit layout
//        xtype: 'grid',
//        border: false,
//        columns: [{ header: 'World'}],// One header just for show. There's no data,
//        store: Ext.create('Ext.data.ArrayStore', {}) // A dummy empty data store
//    }
//}).show();
Ext.create('Ext.window.Window', {
    title: 'Hello',
    id: 'win1',
    height: 1000,
    width: 700,
    //listeners: {
    //  'resize': function (win, width, height, opt) {
    ///  alert(width);

    ///}
    //}

    buttons: [{ text: 'Resize', handler: function (parameters) {
        //get the basic of form
        //var form = this.up('form').getForm();
        //if (form.isValid()) 
        //        {
        //            var myparams1 = new Object();
        //            myparams1.url = "test";
        //            //Ext.lib.Ajax.getHeader = 'application/json';
        //            Ext.Ajax.request({
        //                url: 'http://10.18.22.95:8001/ICodeonService.svc/IICodeonService/pushURL',
        //                params: myparams1,
        //                method: 'GET',
        //                success: function (response, options) {
        //                    // response callback
        //                    // display literal output from response
        //                    var s = response.responseText;
        //                    // decode text into Json object
        //                    var treedata = (Ext.decode(response.responseText)).d;
        //                    //tpl.apply(treedata);
        //                },
        //                failure: function (response, options) {
        //                    Ext.MessageBox.alert('Failed', 'Unable to get');
        //                }

        //                        });



        //        }


        //window.resizeto()

        window.resize(600, 600);
    }
    }],
    buttonAlign: 'center'
}).show();

//
Ext.onReady(function () {
    //    var w = window.innerWidth;
    //    var h = window.innerHeight;
    //    alert(w);
    //    alert(h);
    //    window.resizeTo(400, 400);







    //    Ext.getCmp('win1').setSize().width = 120;

    //    var Ow = window.outerWidth;
    //    var r = window.innerWidth;
    //    var ih = window.innerHeight;
    //    //var b = Ext.getBody().getViewSize();
    //    //var c = b.width;
    //    alert("Ineer width ViewSizebody:" + r);
    //    alert("Outer width ViewSizebody:" + Ow);
    //    Ext.Window.override({
    //        width: 300,
    //        height: 100
    //    });

    //    var rs = window.innerWidth;
    //    alert("Ineer width ViewSizebody:" + rs);

    //    // var b1 = Ext.getBody();
    //    //  var s = b1.getWidth();
    //    //    var ssss = b.getHeight();
    //    //alert("width body:" + s);
    //    //    alert(ssss);
    //    //    var h = Ext.Element.getViewportHeight();
    //    //    var ss = Ext.Element.getViewportWidth();
    //    //    alert(h);
    //    //    alert("width:element" + ss);
    //    //    Ext.getBody().setSize(20, 20);
    //    //    //Ext.Element.setSize(10, 10);

    //    //    var b1 = Ext.getBody();
    //    //    var s1 = b.getWidth();
    //    //    alert(s1);


    //    //    b.setWidth(300);
    //    //    b.setHeight(300);
    //    //    var d = b.getWidth();
    //    //    alert(d);


    //    //    alert(Vwidth);
    //    //    alert(Vheight)
    //    //    // var r = this.getView('win1');
    //    //    // alert(r);
    //    //    var check = 0;
    //    //    if (Vheight > 0 && Vwidth > 0)
    //    //        alert("True");
    //    //    this.window.resizeTo(Vwidth, Vheight);
    //    //    var h = Ext.getCmp('win1').getSize().height;
    //    //    var w = Ext.getCmp('win1').getSize().width;
    //    //    alert(h);
    //    //    alert(w);
    //    //    if (typeof window.innerWidth != 'undefined')
    //    //    { viewportwidth = window.innerWidth, viewportheight = window.innerHeight }
    //    //    // IE6 in standards compliant mode (i.e. with a valid doctype as the first line in the document)    
    //    //    else if (typeof document.documentElement != 'undefined' && typeof document.documentElement.clientWidth != 'undefined' && document.documentElement.clientWidth != 0) { viewportwidth = document.documentElement.clientWidth, viewportheight = document.documentElement.clientHeight }
    //    //    // older versions of IE     
    //    //    else { viewportwidth = document.getElementsByTagName('body')[0].clientWidth, viewportheight = document.getElementsByTagName('body')[0].clientHeight }


    //        var vpSize = Ext.getBody().getViewSize();
    //        alert(vpSize.width);
    //        alert(vpSize.height);
    //        // all Windows created afterwards will have a default value of 90% height and 95% width
    //        Ext.Window.override({
    //            : vpSize.width * 0.9,
    //            : vpSize.height * 0.95
    //        });

    //    var h = Ext.getCmp('win1').getSize().height;
    //        var w = Ext.getCmp('win1').getSize().width;
    //        alert(h);
    //        alert(w);


});
