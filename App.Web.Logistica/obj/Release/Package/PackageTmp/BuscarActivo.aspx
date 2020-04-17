<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.Master" AutoEventWireup="true" CodeBehind="BuscarActivo.aspx.cs" Inherits="App.Web.Logistica.BuscarActivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Buscar activo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <div class="contentPrincipal">
        <table class="tb">
            <tr>
                <td style="width: 100px;">CODIGO:</td>
                <td>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="cajaTexto"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" CssClass="btn btn-primary" />
                </td>
            </tr>
        </table>


        <asp:GridView ID="gvReporte" runat="server" Width="99%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" OnSelectedIndexChanging="gvReporte_SelectedIndexChanging">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="IdLogistica" HeaderText="IdLogistica" />
                <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="Origen" HeaderText="Origen" />
                <asp:BoundField DataField="Detalle" HeaderText="Detalle" />
                <asp:BoundField DataField="Condicion" HeaderText="Condicion" />
            </Columns>
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
            <asp:Button ID="btnSalir" runat="server" OnClick="btnSalir_Click" Text="Salir" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
