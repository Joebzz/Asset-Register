<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AssetRegister.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Login Page</h2>
    </div>
        <asp:Login ID="loginAssetRegister" runat="server" OnAuthenticate="ValidateUser">
        </asp:Login>
    </form>
</body>
</html>
