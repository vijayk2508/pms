<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PMS.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    --%>
    <link rel="stylesheet" href="Content/bootstrap.min.css">
    <script src="Scripts/jquery-3.5.1.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <style>
        .navbar {
            background: black !important;
            border-radius: 0px !important;
              color: white;
        }

            .navbar .navbar-brand {
                font-size: 23px !important;
                font-weight: bold !important;
                padding-top: 12px !important;
                padding-left: 24px !important;
                color: white;
            }

        th, td {
            padding: 10px 5px 4px 9px !important;
        }

        .form-horizontal .control-label {
            text-align: left;
        }

        textarea {
            resize: none;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-light bg-light">
        <a class="navbar-brand" href="#">PMS</a>
    </nav>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="pnlUpdate" runat="server">
            <ContentTemplate>
                <div class="row" style="margin: 0">
                    <div class="col-lg-12">
                        <div class="form-panel">
                            <div class="form-horizontal style-form" style="padding-left: 10px">
                                <div class="form-group">
                                    <asp:HiddenField ID="hdnProductID" runat="server" />
                                    <label class="control-label col-md-2">Product Name<span style="color: red"> *</span></span></label>
                                    <div class="col-md-3 col-xs-11">
                                        <asp:TextBox ID="txtProductName" CssClass="form-control form-control-inline" runat="server" onkeypress="return validateAlphaNumeric(event);"></asp:TextBox>
                                    </div>
                                    <div class="col-md-7 col-xs-11">
                                        <asp:Label ID="lblProductName" runat="server" Style="color: red"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-2">Product Category<span style="color: red"> *</span></label>
                                    <div class="col-md-3 col-xs-11">
                                        <asp:DropDownList ID="ddlProductCategory" CssClass="form-control form-control-inline" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-7 col-xs-11">
                                        <asp:Label ID="lblProductCategory" runat="server" Style="color: red"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-2">Product Description</label>
                                    <div class="col-md-3 col-xs-11">
                                        <asp:TextBox ID="txtProductDescription" CssClass="form-control form-control-inline" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col-md-7 col-xs-11">
                                        <asp:Label ID="lblProductDescription" runat="server" Style="color: red"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-2">Product MRP <span style="color: red">*</span></label>
                                    <div class="col-md-3 col-xs-11">
                                        <asp:TextBox ID="txtProductMRP" CssClass="form-control form-control-inline" runat="server" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                    </div>
                                    <div class="col-md-7 col-xs-11">
                                        <asp:Label ID="lblProductMRP" runat="server" Style="color: red"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-offset-2 col-lg-7">
                                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="this.disabled='true';this.value = 'Please Wait...';" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-theme" runat="server" Text="Cancel" OnClick="btnCancel_Click" OnClientClick="this.disabled='true';this.value = 'Please Wait...';" UseSubmitBehavior="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" style="margin: 0; padding: 0 24px">
                    <asp:GridView ID="gridProduct" runat="server" AutoGenerateColumns="false" Text="true" ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-condensed" OnRowDataBound="gridProduct_RowDataBound" OnPageIndexChanging="gridProduct_PageIndexChanging" PageSize="4" DataKeyNames="Product_Id" GridLines="Horizontal" AllowPaging="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Product ID" SortExpression="ID" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductID" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Name" SortExpression="Product Name"
                                HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Description" SortExpression="Product Descripiton"
                                HeaderStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductDescription" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Category" SortExpression="Product Category"
                                HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductCategory" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product MRP" SortExpression="Product MRP"
                                HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductMRP" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" SortExpression="Action"
                                HeaderStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="btn btn-sm btn-success" Style="margin-top: -6px;" Text="Edit" OnCommand="btnEdit_Command" />
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" CssClass="btn btn-sm btn-danger" Text="Delete" Style="margin-top: -6px;" OnCommand="btnDelete_Command" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div align="center" id="showEmptyDataTemplate">No records found.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:Panel ID="pnlDeleteProduct" class="modal fade" TabIndex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" runat="server" data-backdrop="static" data-keyboard="false">
            <div id="modalDeleteProduct" class="modal-dialog" role="document">
                <asp:UpdatePanel ID="updatePnlDeleteProduct" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-body">
                                <asp:HiddenField ID="hdnID" runat="server" />
                                <div class="form-group">
                                    <label class="col-form-label">Do you want to delete this product ?</label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClientClick="this.disabled='true';this.value = 'Please Wait...';" UseSubmitBehavior="False" OnClick="btnDelete_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    </form>

    <script>
        $(document).ready(function () {
            $('#txtProductName').val('');
            $("#ddlProductCategory").prop('selectedIndex', 0);
            $('#txtProductDescription').val('');
            $('#txtProductMRP').val('');
        })

        $(document).on('hidden.bs.modal', function (e) {
            $('#modalDeleteProduct .modal-body input').each(function () {
                $(this).val('');
            });
        });

        function validateAlphaNumeric(e) {
            //var letters = /^[0-9a-zA-Z]+$/gi;
            //var letters = /^[\w]+([-_\s]{1}[a-z0-9]+)*$/i;
            var letters = /^[ a-zA-Z0-9]+$/;
            if (!(e.key).match(letters))
                return false;
            return true;

        }

        function validateFloatKeyPress(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                return false;
            }
            return true;
        }

        function getSelectionStart(o) {
            if (o.createTextRange) {
                var r = document.selection.createRange().duplicate()
                r.moveEnd('character', o.value.length)
                if (r.text == '') return o.value.length
                return o.value.lastIndexOf(r.text)
            } else return o.selectionStart
        }

    </script>
</body>
</html>



