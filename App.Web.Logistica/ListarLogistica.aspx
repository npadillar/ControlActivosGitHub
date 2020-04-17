<%@ Page Language="C#" MasterPageFile="~/MasterPrincipal.master"  AutoEventWireup="true" CodeBehind="ListarLogistica.aspx.cs" Inherits="App.Web.Logistica.ListarLogistica" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTitle" Runat="Server">
    Mantenimiento de activos
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

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
        function ValidarControles() {

            //alert("ENTRA");
            var TxtCod = $('#<%=TxtCod.ClientID %>');
            alert("pasa");
            if (TxtCod.val() == '') {
                alert('Ingrese el código.')
                TxtCod.focus();
                return false;
            }

            return true;
        }
        </script>   
</asp:Content>
   
 <asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">    
     <div id="dvError" runat="server" visible="false" class="alert-danger dvError" align="center"></div>

     <div style="width: 90%; margin: 0 auto;">

            <table style="width: 950px; margin: 0 auto;">
                <tr>
                    <td colspan="4" class="alignCenter">
                        <h5><b>MANTENIMIENTO DE ACTIVOS</b></h5>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="ckbUpdMasivo" runat="server" Text="&nbsp;Modificación masiva" ForeColor="Red" Font-Bold="true" 
                            AutoPostBack="true" OnCheckedChanged="ckbUpdMasivo_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <th style="width: 120px;">Código Barras:</th>
                    <td>
                        <asp:TextBox ID="TxtCodigo" runat="server" Width="185px" CssClass="cajaTexto" ></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Button ID="BtnBuscar" runat="server" OnClick="BtnBuscar_Click" Text="Buscar activo" CssClass="btn btn-primary" /> 
                    </td>
                    <td style="text-align: right;">
                        <asp:Button ID="BtnModificar" runat="server" OnClick="BtnModificar_Click" Text="Modificar datos" CssClass="btn btn-primary" Enabled="False" Visible="False" />
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
                    <th style="width: 120px;">Código Barras:</th>
                    <td>
                        <asp:TextBox ID="TxtCod" runat="server" MaxLength="11" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <td style="width: 120px;">
                    </td>
                    <td>
                        <asp:CheckBox ID="ckbMasivo" runat="server" Text="&nbsp;Registro masivo" ForeColor="Red" Font-Bold="true" 
                            AutoPostBack="true" OnCheckedChanged="ckbMasivo_CheckedChanged" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <th>Descripción:</th>
                    <td>
                        <asp:TextBox ID="TxtDescripcion" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th>Sede:</th>
                    <td>
                        <asp:DropDownList ID="DDLSede" runat="server" CssClass="cajaTexto">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>Área:</th>
                    <td>
                        <asp:TextBox ID="TxtArea" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th>Piso:</th>
                    <td>
                        <asp:TextBox ID="TxtPiso" runat="server" CssClass="cajaTexto" MaxLength="2" TextMode="Number"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Edificio:</th>
                    <td>
                        <asp:TextBox ID="txtEdificio" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th>Categoria:</th>
                    <td>
                        <asp:DropDownList ID="DDLCategoria" runat="server" CssClass="cajaTexto">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>Aula:</th>
                    <td>
                        <asp:TextBox ID="txtAula" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th>Serie:</th>
                    <td>
                        <asp:TextBox ID="txtSerie" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Estado:</th>
                    <td>
                        <asp:DropDownList ID="DDLCondicion" runat="server" CssClass="cajaTexto" Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <th>Marca</th>
                    <td>
                        <asp:TextBox ID="txtMarca" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Modelo</th>
                    <td>
                        <asp:TextBox ID="txtModelo" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th></th>
                    <td>
                        <asp:TextBox ID="txtLogistica" runat="server" CssClass="cajaTexto" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>RUC:</th>
                    <td>
                        <asp:TextBox ID="txtRuc" runat="server" CssClass="cajaTexto" MaxLength="11" style="width: 85%;"></asp:TextBox>
                         &nbsp;
                        <asp:ImageButton ID="btnRuc" runat="server" ImageUrl="~/Iconos/icon_check.png" OnClick="btnRuc_Click" style="width: 17px; height: 18px;"/> 
                    </td>
                    <th>Fecha Compra:</th>
                    <td>
                        <asp:TextBox ID="txtFechaCompra" runat="server" CssClass="cajaTexto" Enabled="False" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Proveedor:</th>
                    <td>
                        <asp:TextBox ID="txtProveedor" runat="server" CssClass="cajaTexto" placeholder="Razón Social" readonly="true" ></asp:TextBox>
                    </td>
                    <th>Tiempo Garantía:</th>
                    <td>
                        <asp:TextBox ID="txtTiempo" runat="server" CssClass="cajaTexto" Enabled="False" AutoPostBack="true" style="width: 100px;"
                            placeholder="Meses" OnTextChanged="txtTiempo_TextChanged" ></asp:TextBox>                
                        meses
                    </td>                   
                </tr>
                <tr>
                    <th>Número comprobante:</th>
                    <td>
                        <asp:TextBox ID="txtNroFactura" runat="server" CssClass="cajaTexto" MaxLength="15" Enabled="False"></asp:TextBox>
                    </td>
                    <th>Fecha Fin de Garantía:</th>
                    <td>
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="cajaTexto" Enabled="False" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                   <th>Usuario Asignado: </th>
                   <td> 
                       <asp:TextBox ID="txtUsu" runat="server" CssClass="cajaTexto" Enabled="False"></asp:TextBox>
                   </td>
                   <th>Observación:</th>
                   <td>
                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="cajaTexto" MaxLength="11" Enabled="False" TextMode="MultiLine"></asp:TextBox>
                   </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:HiddenField ID="hdId" runat="server" />
                            <asp:HiddenField ID="hdCodigobuscado" runat="server" />
                            <asp:HiddenField ID="hdFecha" runat="server" />
                            <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>

                        <strong>
                            <asp:Label ID="lblnuevo" runat="server" CssClass="auto-style24" Text="NUEVO"></asp:Label>
                            <asp:ImageButton ID="BtnNuevo" runat="server" Height="52px" ImageUrl="~/Iconos/nuevo.jpg" OnClick="BtnNuevo_Click1" Width="50px" />
                        </strong>

                        <strong>
                            <asp:Label ID="lblgrabar" runat="server" CssClass="auto-style24" Text="GRABAR" Visible="False"></asp:Label>
                            <asp:ImageButton ID="BtnGraba" runat="server" Height="47px" ImageUrl="~/Iconos/Save as.gif" OnClick="BtnGraba_Click" Width="42px" Enabled="False" Visible="False" />
                        </strong>
                    </td>

                   <td> 
                       <br /><br />
                       <asp:TextBox ID="txtDireccion" runat="server" Width="268px" Height="23px" Visible="False"></asp:TextBox>
                       <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Style="font-weight: 700" Height="33px" Width="85px" OnClick="btnAgregar_Click" Visible="false" />
                   </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:HiddenField ID="hdIdInventario" runat="server" />

                        <asp:HiddenField ID="hdIdLogistica" runat="server" OnValueChanged="hdIdLogistica_ValueChanged" />

                        <asp:HiddenField ID="hdIdCondicion" runat="server" />

                        <asp:HiddenField ID="hdCondicion" runat="server" />
                        <strong>    

                        <asp:LinkButton ID="LnkNuevo" runat="server" OnClick="LnkNuevo_Click" Visible="False">Nuevo</asp:LinkButton>
    <asp:Button runat="server" ID="btnCentroCosto" Text="Centro-costo" Visible="false"  CssClass="invisible" />

                        </strong>
                    </td>
                </tr>
            </table>


     <div style="text-align: center;">
         <asp:GridView ID="dgvListado" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
             ForeColor="Black" GridLines="Horizontal" Width="100%" AutoGenerateColumns="False" style="font-family: Calibri; font-size: 14px;" OnRowCommand="dgvListado_RowCommand">
             <AlternatingRowStyle BackColor="#CCCCCC" />
             <Columns>
                 <asp:BoundField DataField="codigo" HeaderText="CÓDIGO" />
                 <asp:BoundField DataField="descrip" HeaderText="DESCRIPCIÓN" />
                 <asp:BoundField DataField="area" HeaderText="ÁREA" />
                 <asp:BoundField DataField="edificio" HeaderText="EDIFICIO" />
                 <asp:BoundField DataField="aula" HeaderText="AULA" />
                 <asp:BoundField DataField="modelo" HeaderText="MODELO" />
                 <asp:BoundField DataField="ruc" HeaderText="RUC" />
                 <asp:BoundField DataField="proveedor" HeaderText="PROVEEDOR" />
                 <asp:BoundField DataField="nroFact" HeaderText="NRO COMPR." />
                 <asp:BoundField DataField="sede" HeaderText="SEDE" />
                 <asp:BoundField DataField="piso" HeaderText="PISO" />
                 <asp:BoundField DataField="categoria" HeaderText="CATEGORÍA" />
                 <asp:BoundField DataField="serie" HeaderText="SERIE" />
                 <asp:BoundField DataField="marca" HeaderText="MARCA" />
                 <asp:BoundField DataField="fecCompra" HeaderText="FEC COMPRA" />
                 <asp:BoundField DataField="fecFinGar" HeaderText="FEC FIN GARANTÍA" />
                 <asp:BoundField DataField="observ" HeaderText="OBSERV." />
                 <asp:BoundField DataField="usuAsignado" HeaderText="USU ASIGNADO" />
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

</div>
 </asp:Content>
