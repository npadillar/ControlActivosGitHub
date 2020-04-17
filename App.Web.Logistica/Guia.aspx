<%@ Page Language="C#" MasterPageFile="~/MasterPrincipal.master" AutoEventWireup="true" CodeBehind="Guia.aspx.cs" Inherits="App.Web.Logistica.Guia" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTitle" Runat="Server">
    Guía de activos
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div id="dvError" runat="server" visible="false" class="alert-danger dvError" align="center"></div>
    <asp:HiddenField ID="hdnIdGuia" runat="server" />
        
        <h5 style="text-align: center;"><b>GUÍA DE ACTIVOS</b></h5>

        <table style="width: 950px;" class="tb">
            <tr>
                <td style="width: 150px;">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" CssClass="btn btn-primary" />
                </td>
                <td colspan="3">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" OnClick="btnGuardar_Click" Enabled="False" CssClass="btn btn-primary" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" Enabled="False" OnClick="btnImprimir_Click" CssClass="btn btn-primary" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 10px;">
                </td>
            </tr>
            
              <tr>
                <td>
                    <b</b>
                </td>
                <td>
                    <asp:TextBox ID="txtDireccion" runat="server" Width="268px" Height="23px" Visible="False"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <b><asp:Label ID="ruc" runat="server" Text="RUC:" Visible="false"></asp:Label></b>
                </td>
                <td style="width: 300px;">
                     <asp:TextBox ID="txtRuc" runat="server" CssClass="cajaTexto" MaxLength="11" style="width: 85%;" Visible="false"></asp:TextBox>
                         &nbsp;
                        <asp:ImageButton ID="btnRuc" runat="server" ImageUrl="~/Iconos/icon_check.png" OnClick="btnRuc_Click" style="width: 17px; height: 18px;" Visible="false"/>                    
                </td>
            </tr>
                       
            <tr>
                <td>
                    <b>Motivo del Traslado:</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMotivoTrasldo" runat="server" CssClass="cajaTexto" AutoPostBack="True" OnSelectedIndexChanged="ddlMotivoTrasldo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 200px;">
                    <b>Transportista:</b>
                </td>
                <td style="width: 300px;">
                    <asp:TextBox ID="Transportista" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <b>Punto de partida:</b>
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
            </tr>
            <tr>
                <td>
                    <b>Observación:</b>
                </td>
                <td>
                    <asp:TextBox ID="txtObserv" runat="server" TextMode="MultiLine" CssClass="cajaTexto"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar Activo" OnClientClick="window.open('./buscaractivo.aspx','MiPagina', 'width=1200,height=500,top=200,left=200');" OnClick="btnBuscar_Click" CssClass="btn btn-primary" />
                </td>
                <td>
                    <asp:TextBox ID="txtEstado" runat="server" Visible="False" CssClass="cajaTexto">1</asp:TextBox>
                </td>
            </tr>
        </table>

        <div style="width: 80%; margin: 20px auto 0 auto;">
            <asp:GridView ID="gvDetalle" DataKeyNames="IdUnidadMedida" runat="server" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="IdLogistica" HeaderText="IdLogistica" />
                    <asp:BoundField DataField="CODIGO" HeaderText="Codigo" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                    <asp:BoundField DataField="Origen" HeaderText="Origen" />
                    <asp:BoundField DataField="Detalle" HeaderText="Detalle" />
                    <asp:BoundField DataField="Condicion" HeaderText="Condicion" />
                    <asp:BoundField DataField="IdUnidadMedida" HeaderText="IdUM" Visible="False" />
                    <asp:BoundField DataField="UnidadMedida" HeaderText="Unidad Medida" />
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


            <asp:GridView ID="gvconsulta" runat="server" Width="424px" Visible="False">
            </asp:GridView>
            <asp:HiddenField ID="hdlnomfile" runat="server" />


            <asp:HiddenField ID="hdIdGuia" runat="server" />

            <asp:HiddenField ID="hdusuario" runat="server" />
        </div>
</asp:Content>
