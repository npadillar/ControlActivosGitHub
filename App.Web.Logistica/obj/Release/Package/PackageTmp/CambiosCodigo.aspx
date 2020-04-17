<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.Master" AutoEventWireup="true" CodeBehind="CambiosCodigo.aspx.cs" Inherits="App.Web.Logistica.CambiosCodigo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Cambios de código
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <div class="contentPrincipal" style="text-align: center;">
        <h4>Historial de cambios</h4>
        <asp:GridView ID="dgvListado" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                    BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="500px" style="margin: 0 auto; font-size: 14px;">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>

        <br />
        <asp:Button ID="btnSalir" runat="server" Text="Salir" CssClass="btn btn-primary" OnClick="btnSalir_Click" />
    </div>
</asp:Content>
