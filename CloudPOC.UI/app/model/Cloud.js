Ext.define('State', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'cloudMesageId', type: 'int' },
        { name: 'cloudMessage', type: 'string' },
        { name: 'result', type: 'bool' }
    ]
});