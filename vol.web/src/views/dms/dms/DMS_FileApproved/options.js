// *Author：jxx
// *Contact：xx
// *代码由框架生成,任何更改都可能导致被代码生成器覆盖
export default function(){
    const table = {
        key: 'Id',
        footer: "Foots",
        cnName: '已签文件管理',
        name: 'DMS_FileApproved',
        url: "/DMS_FileApproved/",
        sortName: "FileName,CreateDate"
    };
    const tableName = table.name;
    const tableCNName = table.cnName;
    const newTabEdit = false;
    const key = table.key;
    const editFormFields = {"FileId":"","FileName":"","FileSize":"","Extension":"","FileNote":""};
    const editFormOptions = [[{"title":"已签id","required":true,"field":"FileId"}],
                              [{"title":"文件名","required":true,"field":"FileName","disabled":true}],
                              [{"title":"文件Size","required":true,"field":"FileSize","disabled":true}],
                              [{"title":"后缀","required":true,"field":"Extension","disabled":true}],
                              [{"title":"备注","field":"FileNote","type":"textarea"}]];
    const searchFormFields = {"FileId":"","FileName":"","Hash":""};
    const searchFormOptions = [[{"title":"文件名","field":"FileName"}],[{"title":"已签id","field":"FileId"}],[{"title":"哈希值","field":"Hash"}]];
    const columns = [{field:'Id',title:'ID',type:'guid',width:220,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'FileId',title:'已签id',type:'string',sort:true,width:180,require:true,align:'left',sort:true},
                       {field:'FileName',title:'文件名',type:'string',link:true,width:250,readonly:true,require:true,align:'left'},
                       {field:'Extension',title:'后缀',type:'string',width:60,readonly:true,require:true,align:'left'},
                       {field:'FileSize',title:'文件Size',type:'long',width:120,readonly:true,require:true,align:'left'},
                       {field:'ContentType',title:'类型',type:'string',width:120,hidden:true,align:'left'},
                       {field:'Hash',title:'哈希值',type:'string ',width:220,hidden:true,align:'left'},
                       {field:'MinioBucket',title:'Minio桶',type:'string',width:120,hidden:true,align:'left'},
                       {field:'MinioObject',title:'Minio对象',type:'string',width:220,hidden:true,align:'left'},
                       {field:'Enable',title:'是否可用',type:'short',width:110,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'string ',sort:true,width:220,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,align:'left'},
                       {field:'ModifyID',title:'修改者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Modifier',title:'修改者',type:'string',width:100,hidden:true,align:'left'},
                       {field:'ModifyDate',title:'修改时间',type:'string ',sort:true,width:220,hidden:true,align:'left'},
                       {field:'FileNote',title:'备注',type:'string',width:220,align:'left'}];
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