<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDashborad.aspx.cs" Inherits="WebAPI.Pages.EmployeeDashborad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table{
            border:1px solid #cecece;
        }
        table tr:nth-child(even){
            background:#f5f5f5;
        }
        table td, table th{
            padding:7px 12px;
        }
        table th{
            background:#ddd;
        }
        table tr:nth-child(odd):hover{
            background:#f5f5f5;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gv1" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
