<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="GuiaBienes.aspx.cs" Inherits="App.Web.Logistica.Formulario_web1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTitle" Runat="Server">
    Guía de bienes
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <asp:HiddenField ID="hdnIdGuia" runat="server" />
    <h5 style="text-align: center;"><b>GUÍA DE BIENES</b></h5>

    <table style="width: 950px;" class="tb">
        <tr>
            <td colspan="5">
                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnGuardar" runat="server" Text="Grabar" OnClick="btnGuardar_Click" Enabled="False" CssClass="btn btn-primary" />
                &nbsp;
                <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnImprimir_Click" Enabled="False" CssClass="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td colspan="5" style="height: 10px;"></td>
        </tr>
        <tr>
            <td style="width: 150px;">
                <b>Transportista:</b>
            </td>
            <td>
                <asp:TextBox ID="txtTransportista" runat="server" CssClass="cajaTexto"></asp:TextBox>
            </td>
            <td style="width: 220px;">
                <b>Motivo del Traslado:</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlMotivoTrasldo" runat="server" CssClass="cajaTexto">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtEstado" runat="server" Visible="False">1</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>Punto de Partida:</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlPuntoPartida" runat="server" CssClass="cajaTexto">
                </asp:DropDownList>
            </td>
            <td>
                <b>Punto de Llegada:</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlPuntoLlegada" runat="server" CssClass="cajaTexto">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <b>Unidad de Medida:</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlUnidadMedida" runat="server" CssClass="cajaTexto">
                </asp:DropDownList>
            </td>
            <td>
                <b>Especificar punto de llegada:</b>
            </td>
            <td>
                <asp:TextBox ID="txtllegada" runat="server" CssClass="cajaTexto"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <b>Cantidad:</b>
            </td>
            <td>
                <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" CssClass="cajaTexto"></asp:TextBox>
            </td>
            <td>
                <b>Descripción:</b>
            </td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="cajaTexto"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAdicionar" runat="server" OnClick="btnAdicionar_Click" Text="Agregar" CssClass="btn btn-primary" />
            </td>
        </tr>
        <tr>
            <td>
                <b>Observación:</b>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtObserv" runat="server" TextMode="MultiLine" CssClass="cajaTexto"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: center;">
                <br />
                <asp:GridView ID="gvBienes" DataKeyNames="IdUnidadMedida" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" 
                    BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" Width="90%" style="margin: 0 auto;">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField DataField="cant" HeaderText="Cant" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                        <asp:BoundField DataField="IdUnidadMedida" HeaderText="IdUnidadMedida" Visible="False" />
                        <asp:BoundField DataField="UnidadMedida" HeaderText="UnidadMedida" />
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
            </td>
        </tr>
        <tr>
            <td>
                <strong>

                    <asp:HiddenField ID="hdIdGuia" runat="server" />
                </strong></td>
            <td>
                <strong>

                    <asp:HiddenField ID="hdusuario" runat="server" />
                </strong>
            </td>
            <td class="auto-style8"></td>
            <td class="auto-style3"></td>
            <td class="auto-style3"></td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style9">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
