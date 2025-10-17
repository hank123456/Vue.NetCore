// *Author：jxx
// *Contact：xx
// *代码由框架生成,任何更改都可能导致被代码生成器覆盖
export default function(){
    const table = {
        key: 'FileId',
        footer: "Foots",
        cnName: '我的文件库',
        name: 'DMS_File',
        url: "/DMS_File/",
        sortName: "CreateDate"
    };
    const tableName = table.name;
    const tableCNName = table.cnName;
    const newTabEdit = false;
    const key = table.key;
    const editFormFields = {"FileName":"","Extension":"","FileSize":"","FileNote":""};
    const editFormOptions = [[{"title":"文件名","field":"FileName","disabled":true}],
                              [{"title":"扩展名","field":"Extension","disabled":true}],
                              [{"title":"大小","field":"FileSize","disabled":true}],
                              [{"title":"备注","field":"FileNote","type":"textarea"}]];
    const searchFormFields = {"FileId":"","FileName":"","Hash":""};
    const searchFormOptions = [[{"title":"文件id","field":"FileId"}],[{"title":"文件名","field":"FileName"}],[{"title":"哈希值","field":"Hash"}]];
    const columns = [{field:'FileId',title:'文件id',type:'guid',width:220,readonly:true,require:true,align:'left',sort:true},
                       {field:'FileName',title:'文件名',type:'string',link:true,width:350,readonly:true,align:'left'},
                       {field:'Extension',title:'扩展名',type:'string',width:60,readonly:true,align:'left'},
                       {field:'FileSize',title:'大小',type:'long',width:120,readonly:true,align:'left'},
                       {field:'Hash',title:'哈希值',type:'string ',width:220,readonly:true,align:'left'},
                       {field:'FileNote',title:'备注',type:'string',width:220,align:'left'},
                       {field:'ContentType',title:'文件类型',type:'string',width:120,align:'left'},
                       {field:'MinioBucket',title:'Minio桶',type:'string',width:120,align:'left'},
                       {field:'MinioObject',title:'Minio对象',type:'string',width:220,align:'left'},
                       {field:'Enable',title:'是否可用',type:'short',width:110,hidden:true,align:'left'},
                       {field:'CreateID',title:'创建者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Creator',title:'创建者',type:'string',width:100,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'string ',width:220,align:'left'},
                       {field:'ModifyID',title:'修改者id',type:'int',width:100,hidden:true,align:'left'},
                       {field:'Modifier',title:'修改者',type:'string',width:100,align:'left'},
                       {field:'ModifyDate',title:'修改时间',type:'string ',width:220,align:'left'}];
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