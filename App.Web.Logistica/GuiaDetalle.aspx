<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.Master" AutoEventWireup="true" CodeBehind="GuiaDetalle.aspx.cs" Inherits="App.Web.Logistica.GuiaDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Detalle guía
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <div class="contentPrincipal">
    <h5 style="text-align: center;">
        &nbsp;</h5>
        <h5 style="text-align: center;">
            &nbsp;</h5>
        <h5 style="text-align: center;">
            <b>Número de guía:
            <asp:Label ID="lblNroGuia" runat="server" Text="0"></asp:Label>
        </b>
    </h5>

    <table class="tb">
        <tr>
            <th>
                Usuario que Recepciona Activo:
            </th>
            <td>
                <asp:TextBox ID="txtUsuario" runat="server" Enabled="False" CssClass="cajaTexto"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                Fecha / Hora Recibido:
            </th>
            <td>
                <asp:TextBox ID="txtFecha" runat="server" Enabled="False" CssClass="cajaTexto"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Observación:</b>
                <br />
                <asp:Label ID="lblObserv" runat="server" Text="-"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnRecepcion" runat="server" Text="Registrar Recepción de Activo" OnClick="btnRecepcion_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnReparacion" runat="server" Text="Registrar Reparación" OnClick="btnReparacion_Click" CssClass="btn btn-primary"/>
                <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnImprimir_Click" Enabled="false" CssClass="btn btn-primary" />
                <asp:Button ID="btnAnular" runat="server" OnClick="btnAnular_Click" Text="Anular Guia" CssClass="btn btn-primary" />
                <asp:Button ID="btnExportar" runat="server" Text="Exportar a excel" OnClick="btnExportar_Click" Visible="false" CssClass="btn btn-primary" />
                <asp:TextBox ID="txtEstado" runat="server" Visible="False" Width="16px">2</asp:TextBox>
            </td>
        </tr>
    </table>

    <hr size="3" noshade align="left" style="width: 100%" />

    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Style="font-weight: 700" Text="Detalle de Activo / Bienes"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:GridView ID="gvDetalleGuia" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                    BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="99%" style="margin: 0 auto;">
                
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
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Salir" CssClass="btn btn-primary" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
