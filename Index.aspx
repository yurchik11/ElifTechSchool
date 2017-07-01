<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ElifTechSchool.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Company Tree</title>
    <link rel="stylesheet" href="/bootstrap/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="/bootstrap/css/bootstrap-theme.min.css"/>
    <link rel="stylesheet" href="/font-awesome/css/font-awesome.min.css"/>
    <link type="text/css" rel="stylesheet" href="ContextMenu/jquery.contextMenu.min.css"/>
    <link type="text/css" rel="stylesheet" href="Styles/style.css"/>
    
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true"></asp:ScriptManager>

    <script type="text/x-template" id="item-template">
  <li>
    <div :class="{bold: isFolder}" @click="toggle" @dblclick="changeType" @contextmenu="SaveClickedCompID"> {{model.Name}} | {{model.Earning}} | {{model.EarningWithChild}}
      <span v-if="isFolder">[{{open ? '-' : '+'}}]</span>
    </div>
    <ul v-show="open" v-if="isFolder">
      <item class="item" v-for="model in model.children" :model="model"></item>
    </ul>
  </li>
</script>
<div id="content">
 <button type="button" id="showInfoModal" class="hiid" data-toggle="modal" data-target="#myModal">Open Modal</button>
 <!-- START Modal EDIT -->
<div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Information</h4>
      </div>
      <div class="modal-body">
       <div class="form-group">
                <label class="control-label col-xs-3" for="ID">ID:</label>
                <div class="col-xs-9">
                    <input type="text" class="form-control" name="ID" id="ID" readonly="true"/>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-xs-3" for="Name">Name:</label>
                <div class="col-xs-9">
                    <input type="text" class="form-control" name="Name" id="Name"/>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-xs-3" for="Earning">Earning:</label>
                <div class="col-xs-9">
                    <input type="text" class="form-control" name="Earning" id="Earning"/>
                </div>
            </div>
      </div>
      <div class="modal-footer">
        <button type="button" id="btnSaveAndAdd" class="btn btn-primary" v-on:click="SaveChangesClick">Save Changes</button>
        <button type="button" id="closeModalBtn" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
    <!--END MODAL EDIT-->

</div>
    <div id="myTree" class="myborder">
        <p><i>Please, use the right mouse button to add/edit/delete</i></p>
        <ul id="demo">
            <item class="item context-menu-one" :model="treeData"></item>
        </ul>
    </div>
    </div>
</form>
<script src="/scripts/jquery-3.2.1.min.js"></script>
<script src="bootstrap/js/bootstrap.min.js"></script>
<script src="ContextMenu/jquery.contextMenu.min.js"></script>
<script src="ContextMenu/jquery.ui.position.min.js"></script>
<script src="/scripts/vue.js"></script>
<script src="/scripts/script.js"></script>
</body>
</html>
