<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.Master" AutoEventWireup="true" CodeBehind="detalleGuiaActivo.aspx.cs" Inherits="App.Web.Logistica.detalleGuiaActivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Detalle guía activo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <div class="contentPrincipal">
        <h5 class="alignCenter">
            <b>Codigo:
            <asp:TextBox ID="txtCodigo" runat="server" Enabled="false" CssClass="alignCenter"></asp:TextBox>
            </b>
        </h5>

        <br />
        <asp:GridView ID="gvDetalleActivo" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
            CellPadding="3" ForeColor="Black" GridLines="Vertical" style="margin: 0 auto;" Width="95%">
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
    </div>
</asp:Content>
