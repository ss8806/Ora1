<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sample1.aspx.vb" Inherits="Ora1.sample1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>aspを移植</title>
    <script type="text/javascript">
        function touroku() {
            //クライアント側の入力チェック
            if (document.frmMain.txtName.value == '') {
                alert('商品名を入力してください。');
                return false;
            }
            if (document.frmMain.txtPrice.value == '') {
                alert('単価を入力してください。');
                return false;
            }
            return true;
        }
        function init() {
            if (document.frmMain.hidCheck.value == "1") {
                alert("入力した商品名はすでに登録されています。");
            }
        }
    </script>
</head>
<body onload="init()">
    <form id="frmMain"  method="post"  runat="server">
        <!-- buttonをsubmitコントロールに変更します -->
        <input id="btnTouroku" type="submit" value="登録" onclick="return touroku()" runat="server">
        商品名：<input type="text" name="txtName" id="txtName" runat="server">
        単価：<input type="text" name="txtPrice" id="txtPrice" runat="server">
        <asp:GridView ID="grvDetail" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="商品名" />
            </Columns>
            <Columns>
                <asp:BoundField DataField="PRICE" HeaderText="単価" />
            </Columns>
        </asp:GridView>
        <input type="hidden" name="hidCheck" id="hidCheck" runat="server" />
    </form>
</body>
</html>
