<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.Master" AutoEventWireup="true" CodeBehind="detalleActivo.aspx.cs" Inherits="App.Web.Logistica.detalleActivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Detalle activo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <div style="padding: 20px; font-family: Calibri">
        <h5 class="alignCenter">
            <b>CÓDIGO:
        <asp:TextBox ID="txtCodigo" runat="server" Enabled="False" CssClass="alignCenter"></asp:TextBox>
            </b>
        </h5>

        <b>Ubicacion Actual del Activo</b>
        <asp:GridView ID="gvRep1" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
            ForeColor="Black" GridLines="Vertical">
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

        <b>Modificaciones Anteriores del Activo
        </b>
        <asp:GridView ID="gvReporte" runat="server" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
            BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
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
        <div class="alignCenter">
            <asp:Button ID="btnSalir" runat="server" OnClick="btnSalir_Click1" Text="Salir" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
