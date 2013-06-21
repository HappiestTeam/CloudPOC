Ext.create('Ext.form.Panel', {
    renderTo: document.body,
    title: 'CloudPOC - Push URLs',
    height: 150,
    width: 1000,
    bodyPadding: 10,
    frame: true,
    items: [{ xtype: 'textfield', fieldLabel: 'Enter url', width: 1000, height: 150, id: 'txtUrl',
        allowBlank: false, blankText: 'Field cannot be null', msgTarget: 'under', invalidText: '"{0}" bad. "{1}" good.'
    }],
    buttons: [{ text: 'PushUrl', handler: function (parameters) {
        //get the basic of form
        var form = this.up('form').getForm();
        var txt = Ext.getCmp('txtUrl').getValue();
        if (form.isValid()) {
            Ext.Ajax.request({
                url: 'http://10.18.22.98:9898/api/push?msg=' + txt + '',                
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
    }, { text: 'Search Url', handler: function (parameters) {
        //get the basic of form
        var form = this.up('form').getForm();
        var txt = Ext.getCmp('txtUrl').getValue();
        if (form.isValid()) {
            Ext.Ajax.request({
                url: 'http://10.18.22.98:9898/api/Search?msg=' + txt + '',
                method: 'POST',
                success: function (response, options) {
                    
                    if (response.responseText == "true")
                    { Ext.MessageBox.alert('Enterd Url'+ " " +txt+ " " + 'Found'); }
                    else 
                    {
                    Ext.MessageBox.alert('Unable to Find entered url'+" "+ txt); }
                    
                },
                failure: function (response, options) {
                    Ext.MessageBox.alert('Failed', 'Unable to Find');
                }
            });
        }
    }
    }, { text: 'Clear Text', handler: function (parameters) {
        Ext.getCmp('txtUrl').setValue("");
    }
    }],
    buttonAlign: 'Center'
});

var storeStates = Ext.create('Ext.data.Store', {
    autoLoad: false,
    fields: ['state']
});

Ext.define('State', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'cloudMesageId', type: 'int' },
        { name: 'cloudMessage', type: 'string' },
        { name: 'result', type: 'bool' }
    ]
});

Ext.create('Ext.grid.Panel', {
    renderTo: Ext.getBody(),
    store: storeStates,
    width: 1000,
    height: 500,
    title: 'Response Result',
    columns: [
        {
            text: 'Id',
            width: 100,
            sortable: false,
            hideable: false,
            dataIndex: 'cloudMesageId'
        },
        {
            text: 'Url',
            width: 150,
            dataIndex: 'cloudMessage',
            hidden: false
        },
        {
            text: 'Process Status',
            width: 150,
            dataIndex: 'result',
            hidden: false
        }

    ]
});








