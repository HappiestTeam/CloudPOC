Ext.define('App.view.Viewport', {
    extend: 'Ext.container.Viewport',
    layout: 'fit',
    items: [{
        xtype: 'panel',
        title: 'Icodean Push Url Solution mvc',      
        frame: true,
        items: [{ xtype: 'textfield', fieldLabel: 'Enter url', width: 1000, height: 150, id: 'txtUrl',
        allowBlank: false, blankText: 'Field cannot be null', msgTarget: 'under', invalidText: '"{0}" bad. "{1}" good.'
    }], buttons: [{ text: 'PushUrl', handler: function (parameters) {
        //get the basic of form
        var form = this.up('form').getForm();
        var txt = Ext.getCmp('txtUrl').getValue();
        if (form.isValid()) {
            Ext.Ajax.request({
                url: 'http://localhost:6256/api/push?msg=' + txt + '',
                method: 'POST',
                success: function (response, options) {
                    var jsonData = Ext.JSON.decode(response.responseText);
                    storeStates.loadData(jsonData);
                },
                failure: function (response, options) {
                    Ext.MessageBox.alert('Failed', 'Unable to get');
                }
            });
        }
    }
    }, { text: 'Search Url' }, { text: 'Clear Text', handler: function (parameters) {
        Ext.getCmp('txtUrl').setValue("");
    }
    }],
    buttonAlign: 'Center'
    }]
});