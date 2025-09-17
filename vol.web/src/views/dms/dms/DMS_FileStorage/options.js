// *Author：jxx
// *Contact：xx
// *代码由框架生成,任何更改都可能导致被代码生成器覆盖
export default function(){
    const table = {
        key: 'Id',
        footer: "Foots",
        cnName: '源文件管理',
        name: 'DMS_FileStorage',
        url: "/DMS_FileStorage/",
        sortName: "CreateDate"
    };
    const tableName = table.name;
    const tableCNName = table.cnName;
    const newTabEdit = false;
    const key = table.key;
    const editFormFields = {"Id":"","FileGroupId":"","OriginalFileName":"","FileName":"","FileSize":"","Extension":"","ContentType":"","Hash":"","MinioBucket":"","MinioObject":"","Major":"","Minor":"","Revision":"","Version":"","IsCurrentVersion":"","VersionComment":"","Enable":""};
    const editFormOptions = [[{"title":"id","required":true,"field":"Id","disabled":true},
                               {"title":"文件id","field":"FileGroupId"}],
                              [{"title":"文件名","required":true,"field":"FileName"},
                               {"title":"源文件名","field":"OriginalFileName","disabled":true}],
                              [{"title":"文件大小（字节）","required":true,"field":"FileSize","disabled":true},
                               {"title":"扩展名","required":true,"field":"Extension","disabled":true}],
                              [{"title":"类型","field":"ContentType","disabled":true},
                               {"title":"哈希值","field":"Hash","disabled":true}],
                              [{"title":"文件桶","required":true,"field":"MinioBucket","disabled":true},
                               {"title":"文件对象","required":true,"field":"MinioObject","disabled":true}],
                              [{"title":"主版本号","required":true,"field":"Major","type":"text"},
                               {"title":"次版本号","required":true,"field":"Minor","type":"text"}],
                              [{"title":"修订号","required":true,"field":"Revision","type":"text"},
                               {"title":"版本","field":"Version","disabled":true}],
                              [{"dataKey":"enable","data":[],"title":"是否最新版","required":true,"field":"IsCurrentVersion","type":"select"}],
                              [{"title":"版本说明","field":"VersionComment","type":"textarea"}],
                              [{"dataKey":"enable","data":[],"title":"是否可用","required":true,"field":"Enable","type":"select"}]];
    const searchFormFields = {"FileGroupId":"","OriginalFileName":"","FileName":"","Hash":""};
    const searchFormOptions = [[{"title":"文件id","field":"FileGroupId"},{"title":"源文件名","field":"OriginalFileName"},{"title":"文件名","field":"FileName"}],[{"title":"哈希值","field":"Hash"}]];
    const columns = [{field:'Id',title:'id',type:'guid',sort:true,width:220,readonly:true,require:true,align:'left',sort:true},
                       {field:'FileGroupId',title:'文件id',type:'guid',sort:true,width:220,align:'left'},
                       {field:'FileName',title:'文件名',type:'string',link:true,width:220,require:true,align:'left'},
                       {field:'OriginalFileName',title:'源文件名',type:'string',width:220,readonly:true,align:'left'},
                       {field:'FileSize',title:'文件大小（字节）',type:'long',width:150,readonly:true,require:true,align:'left'},
                       {field:'Extension',title:'扩展名',type:'string',width:60,readonly:true,require:true,align:'left'},
                       {field:'ContentType',title:'类型',type:'string',width:120,hidden:true,readonly:true,align:'left'},
                       {field:'Hash',title:'哈希值',type:'string ',width:220,readonly:true,align:'left'},
                       {field:'MinioBucket',title:'文件桶',type:'string',width:120,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'MinioObject',title:'文件对象',type:'string',width:220,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'Major',title:'主版本号',type:'int',width:110,hidden:true,require:true,align:'left'},
                       {field:'Minor',title:'次版本号',type:'int',width:110,hidden:true,require:true,align:'left'},
                       {field:'Revision',title:'修订号',type:'int',width:110,hidden:true,require:true,align:'left'},
                       {field:'Version',title:'版本',type:'string',width:110,readonly:true,align:'left'},
                       {field:'IsCurrentVersion',title:'是否最新版',type:'int',bind:{ key:'enable',data:[]},width:220,require:true,align:'left'},
                       {field:'VersionComment',title:'版本说明',type:'string',width:220,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,readonly:true,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'string ',sort:true,width:220,readonly:true,align:'left'},
                       {field:'ModifyID',title:'修改者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Modifier',title:'修改者',type:'string',width:100,readonly:true,align:'left'},
                       {field:'ModifyDate',title:'修改时间',type:'string ',width:220,readonly:true,align:'left'},
                       {field:'Enable',title:'是否可用',type:'int',bind:{ key:'enable',data:[]},width:110,require:true,align:'left'},
                       {field:'AuditStatus',title:'审核状态',type:'int',width:80,align:'left'},
                       {field:'AuditId',title:'审核人id',type:'int',width:80,hidden:true,align:'left'},
                       {field:'Auditor',title:'审核人',type:'string',width:120,hidden:true,align:'left'},
                       {field:'AuditDate',title:'审核时间',type:'string ',width:220,hidden:true,align:'left'},
                       {field:'AuditRemark',title:'审核记录',type:'string',width:220,hidden:true,align:'left'}];
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