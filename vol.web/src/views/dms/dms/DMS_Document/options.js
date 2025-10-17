// *Author：jxx
// *Contact：xx
// *代码由框架生成,任何更改都可能导致被代码生成器覆盖
export default function(){
    const table = {
        key: 'DocumentId',
        footer: "Foots",
        cnName: '我的文档',
        name: 'DMS_Document',
        url: "/DMS_Document/",
        sortName: "CreateDate,DocName"
    };
    const tableName = table.name;
    const tableCNName = table.cnName;
    const newTabEdit = false;
    const key = table.key;
    const editFormFields = {"DocType":"","DocName":"","DocCode":"","Version":"","Status":"","Enable":"","Description":""};
    const editFormOptions = [[{"title":"文档名称","required":true,"field":"DocName","type":"text"},
                               {"dataKey":"DocType","data":[],"title":"文档类型","required":true,"field":"DocType","type":"select"}],
                              [{"title":"文档编码","field":"DocCode","disabled":true},
                               {"title":"版本","field":"Version","disabled":true}],
                              [{"dataKey":"DocStatus","data":[],"title":"状态","required":true,"field":"Status","disabled":true,"type":"select"},
                               {"dataKey":"enable","data":[],"title":"是否可用","required":true,"field":"Enable","disabled":true,"type":"select"}],
                              [{"title":"文档说明","field":"Description","colSize":12,"type":"textarea"}]];
    const searchFormFields = {"DocCode":"","DocType":"","DocName":"","Version":""};
    const searchFormOptions = [[{"title":"文档编码","field":"DocCode","type":"like"},{"title":"文档名称","field":"DocName","type":"like"}],[{"dataKey":"DocType","data":[],"title":"文档类型","field":"DocType","type":"select"},{"title":"版本","field":"Version"}]];
    const columns = [{field:'DocumentId',title:'文档id',type:'guid',width:220,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'DocCode',title:'文档编码',type:'string',width:150,readonly:true,align:'left',sort:true},
                       {field:'DocType',title:'文档类型',type:'string',bind:{ key:'DocType',data:[]},width:140,require:true,align:'left'},
                       {field:'DocName',title:'文档名称',type:'string',link:true,width:220,require:true,align:'left'},
                       {field:'Version',title:'版本',type:'string',width:50,readonly:true,align:'left'},
                       {field:'Status',title:'状态',type:'string',bind:{ key:'DocStatus',data:[]},width:70,readonly:true,require:true,align:'left'},
                       {field:'Description',title:'文档说明',type:'string',width:220,align:'left'},
                       {field:'Enable',title:'是否可用',type:'short',bind:{ key:'enable',data:[]},width:110,readonly:true,require:true,align:'left'},
                       {field:'AuditId',title:'审核id',type:'int',width:110,align:'left'},
                       {field:'AuditStatus',title:'审核状态',type:'int',width:140,align:'left'},
                       {field:'Auditor',title:'审核人',type:'string',width:180,align:'left'},
                       {field:'AuditDate',title:'审核时间',type:'datetime',width:150,align:'left',sort:true},
                       {field:'AuditReason',title:'审核备注',type:'string',width:220,align:'left'},
                       {field:'CreateID',title:'创建者id',type:'int',width:100,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'string ',width:220,align:'left'},
                       {field:'ModifyID',title:'修改者id',type:'int',width:100,align:'left'},
                       {field:'Modifier',title:'修改者',type:'string',width:100,align:'left'},
                       {field:'ModifyDate',title:'修改时间',type:'string ',width:220,align:'left'}];
    const detail =  {
                    cnName: '文档文件关联',
                    table: 'DMS_DocumentFile',
                    columns: [{field:'Id',title:'ID',type:'guid',width:220,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'Sequence',title:'序号',type:'int',width:110,edit:{type:'number'},require:true,align:'left',sort:true},
                       {field:'DocumentId',title:'文档id',type:'guid',width:220,hidden:true,require:true,align:'left'},
                       {field:'FileId',title:'文件id',type:'guid',bind:{ key:'FileList',data:[]},width:220,edit:{type:'select'},require:true,align:'left'},
                       {field:'FileType',title:'文件类型',type:'string',bind:{ key:'FileType',data:[]},width:110,edit:{type:'select'},require:true,align:'left'},
                       {field:'Enable',title:'文件可用',type:'short',width:110,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'string ',width:220,align:'left'},
                       {field:'ModifyID',title:'编辑者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Modifier',title:'编辑者',type:'string',width:100,hidden:true,align:'left'},
                       {field:'ModifyDate',title:'编辑时间',type:'string ',width:220,hidden:true,align:'left'}],
                    sortName: 'CreateDate',
                    key: 'Id'
                                            };
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