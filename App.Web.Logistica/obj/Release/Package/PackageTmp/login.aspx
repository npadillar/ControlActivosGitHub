<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="App.Web.Logistica.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<meta http-equiv="Refresh" content="0.1;url=https://sistemas.sise.com.pe/sistemas/login.aspx" />--%>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>SISE - Sistema Control Activos</title>
      <link rel='shortcut icon' type='image/x-icon' href="Iconos/sise.ico" />
<%--    <script type ="text/javascript">
        function EnterEvent(e) {
            if (e.keyCode == 13) {
               
                //document.getElementById('<%=BtnLogin.ClientID%>').click();
                var objO = document.getElementByid("BtnLogin");

                objO.click();
        }
    }
    </script>--%>
    <style type="text/css">
        .auto-style1 {
            border-style: double;
            border-color: inherit;
            border-width: medium;
            width: 100%;
            margin-bottom: 14px;
            height: 629px;
            background-color: gray;
            background-position: center;
        }

        .auto-style2 {
            text-align: center;
            font-size: xx-large;
            width: 609px;
            height: 47px;
        }

        .auto-style14 {
            text-align: center;
            font-size: x-large;
            width: 609px;
            height: 25px;
        }

        .auto-style19 {
            width: 375px;
            height: 47px;
        }

        .auto-style25 {
            width: 609px;
            text-align: center;
            height: 18px;
        }

        .auto-style26 {
            width: 375px;
            height: 18px;
        }

        .auto-style27 {
            height: 18px;
        }

        .auto-style28 {
            width: 375px;
            height: 63px;
        }

        .auto-style29 {
            height: 63px;
            width: 609px;
            text-align: center;
        }

        .auto-style30 {
            height: 63px;
        }

        .auto-style31 {
            width: 375px;
            height: 23px;
        }

        .auto-style32 {
            height: 23px;
            width: 609px;
            text-align: center;
        }

        .auto-style33 {
            height: 23px;
        }

        .auto-style34 {
            width: 375px;
            height: 14px;
        }

        .auto-style35 {
            width: 609px;
            text-align: center;
            height: 14px;
        }

        .auto-style36 {
            height: 14px;
        }
        .auto-style37 {
            width: 375px;
            height: 25px;
        }
        .auto-style38 {
            text-align: center;
        }
    </style>
</head>
<body style="height: 706px">
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style37">
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="66px" ImageUrl="~/Iconos/logo.png" Width="212px" />
                </td>
                <td class="auto-style14"></td>
                <td class="auto-style38" rowspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style19"></td>
                <td class="auto-style2"><strong>Sistema de Control Activos</strong></td>
            </tr>
            <tr>
                <td class="auto-style31"></td>
                <td class="auto-style32">
                    <asp:TextBox ID="TxtUsuario"  placeholder="Usuario" runat="server" Height="29px" Width="171px"></asp:TextBox>
                </td>
                <td class="auto-style33"></td>
            </tr>
            <tr>
                <td class="auto-style34"></td>
                <td class="auto-style35">
                    <asp:TextBox ID="TxtClave"  placeholder="Clave" runat="server" Height="26px" Width="171px" TextMode="Password"></asp:TextBox>
                </td>
                <td class="auto-style36"></td>
            </tr>
            <tr>
                <td class="auto-style34"></td>
                <td class="auto-style35">
                    <asp:DropDownList ID="cboSede" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style36"></td>
            </tr>
            <tr>
                <td class="auto-style26"></td>
                <td class="auto-style25"></td>
                <td class="auto-style27"></td>
            </tr>
            <tr>
                <td class="auto-style28"></td>
                <td class="auto-style29">
                    <asp:Button ID="BtnLogin" runat="server" Style="font-weight: 700; margin-right: 14px; margin-bottom: 0px;" Text="Aceptar" Height="40px" Width="170px" Font-Size="Large" OnClick="BtnLogin_Click" />
                </td>
                <td class="auto-style30">&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
