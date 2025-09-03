// *Author：jxx
// *Contact：xx
// *代码由框架生成,任何更改都可能导致被代码生成器覆盖
export default function(){
    const table = {
        key: 'DomDetailId',
        footer: "Foots",
        cnName: 'BOM明细',
        name: 'MES_Bom_Detail',
        url: "/MES_Bom_Detail/",
        sortName: "CreateDate"
    };
    const tableName = table.name;
    const tableCNName = table.cnName;
    const newTabEdit = false;
    const key = table.key;
    const editFormFields = {"UsageQty":""};
    const editFormOptions = [[{"title":"单台用量","required":true,"field":"UsageQty","type":"number"}]];
    const searchFormFields = {};
    const searchFormOptions = [];
    const columns = [{field:'UsageQty',title:'单台用量',type:'decimal',width:90,require:true,align:'left',sort:true},
                       {field:'DomDetailId',title:'DomDetailId',type:'string',width:220,require:true,align:'left'},
                       {field:'BomId',title:'BomId',type:'string',width:220,align:'left'},
                       {field:'Sequence',title:'Sequence',type:'int',width:80,require:true,align:'left'},
                       {field:'MaterialCode',title:'MaterialCode',type:'string',link:true,width:110,align:'left'},
                       {field:'MaterialName',title:'MaterialName',type:'string',width:220,align:'left'},
                       {field:'Spec',title:'Spec',type:'string',width:110,align:'left'},
                       {field:'ConsumeModel',title:'ConsumeModel',type:'string',width:120,align:'left'},
                       {field:'Warehouse',title:'Warehouse',type:'string',width:120,align:'left'},
                       {field:'SupplierCode',title:'SupplierCode',type:'string',width:110,align:'left'},
                       {field:'KitScale',title:'KitScale',type:'decimal',width:220,align:'left'},
                       {field:'Enable',title:'Enable',type:'int',width:80,align:'left'},
                       {field:'CreateID',title:'CreateID',type:'int',width:80,align:'left'},
                       {field:'Creator',title:'Creator',type:'string',width:110,align:'left'},
                       {field:'CreateDate',title:'CreateDate',type:'string ',width:220,align:'left'},
                       {field:'ModifyID',title:'ModifyID',type:'int',width:80,align:'left'},
                       {field:'Modifier',title:'Modifier',type:'string',width:110,align:'left'},
                       {field:'ModifyDate',title:'ModifyDate',type:'string ',width:220,align:'left'}];
    const detail ={columns:[]};
    const details = [];

    return {
        table,
        key,
        tableName,
        tableCNName,
        newTabEdit,
        editFormFields,
        editFormOptions,
        searchFormFields,
        searchFormOptions,
        columns,
        detail,
        details
    };
}