<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="updateMasivo.aspx.cs" Inherits="App.Web.Logistica.updateMasivo" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTitle" Runat="Server">
    Modificación masiva activos
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".modalPopUpUF").fancybox({
                'autoDimensions': false,
                'width': 400,
                'height': 450,
                'autoScale': false,
                'transitionIn': 'none',
                'transitionOut': 'none',
                'type': 'iframe',
                'showCloseButton': false
                , 'onClosed': function () { BuscarCC(); }
            });

            ModalPopPup('modalPopUp', 800, 600);
        });

        function BuscarCC() {
            var btn = $('#<%=btnCentroCosto.ClientID %>');
            btn.click();
        }
        </script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div id="dvError" runat="server" class="alert alert-danger alert-dismissible show alignCenter" role="alert" visible="false">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>


         <h5 class="alignCenter" style="margin: 0px;"><b>MODIFICACIÓN MASIVA DE ACTIVOS</b></h5>
    <br />
            <table style="width: 950px; margin: 0 auto;">
                <tr>
                    <th style="width: 120px;">Código Barras:</th>
                    <td>
                        <asp:TextBox ID="TxtCodigo" runat="server" CssClass="cajaTexto" Width="200px"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Button ID="BtnBuscar" runat="server" OnClick="BtnBuscar_Click" Text="Buscar y agregar" CssClass="btn btn-primary" />
                    </td>
                    <td style="text-align: right;">
                        <asp:Button ID="BtnModificar" runat="server" OnClick="BtnModificar_Click" Text="Modificar" Enabled="False" Visible="False" CssClass="btn btn-primary" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <HR SIZE=3 noshade align="left">
                    </td>
                </tr>
                </table>
                    
               <table style="width: 950px;" class="tb">
                <tr>
                    <th style="width: 100px;">Área:</th>
                    <td>
                        <asp:TextBox ID="TxtArea" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th style="width: 100px;">Piso:</th>
                    <td>
                        <asp:TextBox ID="TxtPiso" runat="server" MaxLength="2" TextMode="Number" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Edificio:</th>
                    <td>
                        <asp:TextBox ID="txtEdificio" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th style="width: 10%">Sede:</th>
                    <td>    
                        <asp:DropDownList ID="DDLSede" runat="server" CssClass="cajaTexto">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>Aula:</th>
                    <td>
                        <asp:TextBox ID="txtAula" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th>Fecha Compra:</th>
                    <td>
                        <asp:TextBox ID="txtFechaCompra" runat="server" CssClass="cajaTexto" Enabled="False" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>RUC:</th>
                    <td>
                        <asp:TextBox ID="txtRuc" runat="server" CssClass="cajaTexto" MaxLength="11" style="width: 88%;"></asp:TextBox>
                         &nbsp;
                        <asp:ImageButton ID="btnRuc" runat="server" ImageUrl="~/Iconos/icon_check.png" OnClick="btnRuc_Click" style="width: 17px; height: 18px;"/> 
                    </td>
                </tr>
                <tr>
                    <th>Proveedor:</th>
                    <td>
                        <asp:TextBox ID="txtProveedor" runat="server" CssClass="cajaTexto" placeholder="Razón Social" readonly="true"></asp:TextBox>
                    </td>
                    <th>Tiempo de Garantía:</th>
                    <td>
                        <asp:TextBox ID="txtTiempo" runat="server" CssClass="cajaTexto" Enabled="False" AutoPostBack="true" placeholder="Meses" OnTextChanged="txtTiempo_TextChanged" ></asp:TextBox>                
                    </td>                   
                </tr>

                <tr>
                    <th th class="auto-style17">Nª Factura:</th>
                    <td style="vertical-align: top;"> 
                       <asp:TextBox ID="txtNroFactura" runat="server" CssClass="cajaTexto" MaxLength="15" ></asp:TextBox>
                    </td>
                    <th class="auto-style3">Fecha Fin de Garantía:</th>
                    <td class="auto-style28">
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="cajaTexto" Enabled="False" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                   <tr>
                        <th style="vertical-align: top;">Usuario Asignado: </th>
                    <td style="vertical-align: top;"> 
                       <asp:TextBox ID="txtUsu" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                       <td>

                       </td>
                   </tr>
                <tr>
                    <td colspan="4">
                        <asp:HiddenField ID="hdId" runat="server" />
                            <asp:HiddenField ID="hdCodigobuscado" runat="server" />
                            <asp:HiddenField ID="hdFecha" runat="server" />
                            <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>



                        <asp:HiddenField ID="hdIdInventario" runat="server" />

                        <asp:HiddenField ID="hdIdLogistica" runat="server" OnValueChanged="hdIdLogistica_ValueChanged" />

                        <asp:HiddenField ID="hdIdCondicion" runat="server" />

                        <asp:HiddenField ID="hdCondicion" runat="server" />
                        <strong>    

    <asp:Button runat="server" ID="btnCentroCosto" Text="Centro-costo" Visible="false"  CssClass="invisible" />

                        </strong>
                    </td>
                </tr>
            </table>

         <br />
     <div style="width: 900px; margin: 0 auto; text-align: center;">
         <asp:GridView ID="dgvListado" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
             ForeColor="Black" GridLines="Horizontal" Width="100%" AutoGenerateColumns="False" style="font-family: Calibri; font-size: 14px;" OnRowCommand="dgvListado_RowCommand">
             <AlternatingRowStyle BackColor="#CCCCCC" />
             <Columns>
                 <asp:BoundField DataField="codigo" HeaderText="CÓDIGO" />
                 <asp:BoundField DataField="descrip" HeaderText="DESCRIPCIÓN" />
                 <asp:BoundField DataField="Ruc" HeaderText="RUC" Visible="false" />
                 <asp:BoundField DataField="nroFact" HeaderText="NRO FACT" Visible="false" />
                 <asp:BoundField DataField="fecCompra" HeaderText="FEC COMPRA" Visible="false" />
                 <asp:BoundField DataField="observ" HeaderText="OBSERV." />
                 <asp:ButtonField ButtonType="Image" CommandName="quitar" ImageUrl="~/Iconos/x.png" HeaderImageUrl="~/Iconos/tachito.png">
                    <HeaderStyle Width="10px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" />
                 </asp:ButtonField>
             </Columns>
             <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
             <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
             <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
             <SortedAscendingCellStyle BackColor="#F7F7F7" />
             <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
             <SortedDescendingCellStyle BackColor="#E5E5E5" />
             <SortedDescendingHeaderStyle BackColor="#242121" />
         </asp:GridView>
</div>
</asp:Content>
