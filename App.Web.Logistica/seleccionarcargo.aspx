<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="seleccionarcargo.aspx.cs" Inherits="App.Web.Logistica.seleccionarcargo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br /><br /><br /><br />
        <div class="contentLogin" style="width: 550px; margin: 0 auto;">
            <h4 class="alignCenter">Seleccione el cargo con el que va a ingresar al sistema</h4>
            <br />
            <div id="dvError" runat="server" visible="false" class="alert-danger dvError"></div>
    <asp:GridView ID="dgvListado" runat="server" CellPadding="4" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" 
                    BorderStyle="None" BorderWidth="1px" Width="100%" CssClass="alignCenter" OnRowCommand="dgvListado_RowCommand" DataKeyNames="idCar">
                    <Columns>
                        <asp:BoundField DataField="cargo" HeaderText="CARGO" />
                        <asp:ButtonField ButtonType="Image" CommandName="seleccionar" HeaderText="" ImageUrl="~/Iconos/check.png" >
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:ButtonField>
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                </asp:GridView>
            <br />
        </div>
    </form>
</body>
</html>
