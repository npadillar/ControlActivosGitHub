<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reparaciones.aspx.cs" Inherits="App.Web.Logistica.Reparaciones" %>

<%@ Import Namespace="Logistica.Libreria.Negocio" %>
<%@ Import Namespace="Logistica.Libreria.Entidad" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Reparaciones y Garantía</title>
    <link rel='shortcut icon' type='image/x-icon' href="Iconos/sise.ico" />
    <link rel="Stylesheet" href="Scripts/estilos.css" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //agregar encabezado
            $('#gvGuia tbody tr:first').wrap('<thead/>').parent().prependTo('#gvGuia');

            $("#gvGuia tbody").on('click', 'tr', function () {
                $("#gvGuia tbody tr").removeClass("resaltar");
                $(this).addClass("resaltar");
            });
        });
    </script>
    <style type="text/css">
        .resaltar {
            background: #acd5f7;
        }

        #gvGuia thead, tbody tr, tfoot tr {
            display: table;
            width: 100%;
            /*el ancho de cada columna permanecerá fijo, del mismo ancho que las columnas del cuerpo*/
            table-layout: fixed;
        }

        #gvGuia thead {
            /*el ancho de la fila del encabezado sera todo menos el ancho del scroll vertical*/
            width: 95%;
            text-align: center;
        }

        #gvGuia {
            width: 95%;
        }

            #gvGuia tbody {
                /*max-height: 50px;*/
                /*mostrar el cuerpo*/
                display: block;
                /*altura del cuerpo*/
                height: 500px;
                /*overflow:auto;*/
                /*mostrar la barra vertical del cuerpo*/
                overflow-y: auto;
                /*ocultar la barra horizontal del cuerpo*/
                overflow-x: hidden;
                text-align: center;
            }

            #gvGuia tr td {
                /*ajustar el contenido al ancho de la columna del cuerpo*/
                word-wrap: break-word;
            }

            #gvGuia thead tr th {
                /*ajustar el contenido al ancho de cada columna del encabezado*/
                word-wrap: break-word;
            }

            #gvGuia thead {
                /*color para el encabezado*/
                background-color: Silver;
            }

            #gvGuia tfoot {
                /*color para el pie*/
                background-color: Silver;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
   <%--      <div id="Pagina">
            <div class="headerPrincipal">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 150px;">
                            <asp:Image ID="imgTitulo" ImageUrl="~/Iconos/logo_black.png" runat="server" />
                        </td>
                        <td>
                            <b>SISTEMA DE CONTROL ACTIVOS</b>
                        </td>
                        <td style="width: 200px; text-align: right;">
                            <b>Usuario:
                        <asp:Label ID="lblUsuario" runat="server" Style="text-align: center" Text="" /></b>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div style="width: 80%; margin: 0 auto;">
                <nav class="navbar navbar-expand-lg navbar-light bg-light" style="border: 2px solid #097dca; border-radius: 5px 5px; background: #fafafa !important;">
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>

                    <div class="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul class="navbar-nav mr-auto" style="margin: 0 auto;">
                            <li class="nav-item active">
                                <a class="nav-link" href="https://sistemas.sise.com.pe/sistemas/index.aspx">Menú principal&nbsp;&nbsp;&nbsp;&nbsp;|</a>
                            </li>
                            <%
                                MenuN objMenu = new MenuN();

                                List<MenuEst.EST_MODULO> estMod = new List<MenuEst.EST_MODULO>();
                                List<MenuEst.EST_PAGINA> estPag = new List<MenuEst.EST_PAGINA>();
                                estMod = objMenu.fun_listarModulos_xSistema_xCargo(Convert.ToInt16(Session["cargo"]));

                                foreach (var m in estMod)
                                { %>
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><%=m.descrip %></a>
                                <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <%
                                        estPag = objMenu.fun_listarPaginas_xModulo_xCargo(m.idMod, Convert.ToInt16(Session["cargo"]));

                                        foreach (var p in estPag)
                                        {
                                    %>
                                    <a class="dropdown-item" href="<%=p.link %>"><%=p.descrip %></a>
                                    <div class="dropdown-divider"></div>
                                    <% }  %>
                                </div>
                            </li>
                            <%  } %>
                            <li class="nav-item active">
                                <a class="nav-link" href="salir.aspx">|&nbsp;&nbsp;&nbsp;&nbsp;Cerrar sesión</a>
                            </li>
                        </ul>
                    </div>
                </nav>
            </div>--%>

            <div class="contentPrincipal">
                 <div id="dvError" runat="server" visible="false" class="alert-danger dvError" align="center"></div>
                <h5 class="alignCenter"><b>CONTROL DE GARANTÍA Y REPARACIONES</b></h5>

                <table style="width: 980px;" class="tb">
                   <tr>
                        <th style="width: 190px;">
                             N° de Guía:
                        </th>
                        <td>
                            
                            <b>
                            <asp:Label ID="lblGuia" runat="server" Text="0"></asp:Label>
                            </b>
                            
                        </td>
                        <th style="width: 190px;">
                            Fecha Reparación:
                        </th>
                        <td>
                            <asp:TextBox ID="txtFecReparacion" runat="server" CssClass="cajaTexto" Enabled="true" TextMode="Date"></asp:TextBox>        
                        </td>
                    </tr>
                    
                    <tr>
                        <th style="width: 190px;">
                            Usuario que Recepciona:
                        </th>
                        <td>
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="cajaTexto" Enabled="false"></asp:TextBox>
                        </td>
                        <th style="width: 190px;">
                            Tiempo de Garantía:
                        </th>
                        <td>
                              <asp:TextBox ID="txtTiempo" runat="server" CssClass="cajaTexto" Enabled="true" AutoPostBack="true" style="width: 100px;"
                            placeholder="Meses" OnTextChanged="txtTiempo_TextChanged" ></asp:TextBox>                
                        meses
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 190px;">
                            Fecha / Hora Recibido:
                        </th>
                        <td>
                            <asp:TextBox ID="txtFecha" runat="server" Enabled="False" CssClass="cajaTexto"></asp:TextBox> 
                        </td>
                        <th style="width: 190px;">
                            Fecha Fin de Garantía:
                        </th>
                        <td>
                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="cajaTexto" Enabled="False" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 190px;">
                            Proveedor:
                        </th>
                        <td>
                            <asp:TextBox ID="txtProveedor" runat="server" CssClass="cajaTexto" placeholder="Razón Social" readonly="true" ></asp:TextBox>
                    
                        </td>
                        <th style="width: 190px;">
                            Total:
                        </th>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="cajaTexto" AutoPostBack="true" Enabled="false" text="0.00"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 190px;">
                            N° Comprobante:
                        </th>
                        <td>
                            <asp:TextBox ID="txtNroFactura" runat="server" CssClass="cajaTexto" Enabled="true" MaxLength="15"></asp:TextBox>
                        </td>
                        <th style="width: 190px;">
                            Observación:
                        </th>
                        <td>
                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="cajaTexto" MaxLength="150" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 190px;">
                            Fecha de Envío:
                        </th>
                        <td>
                            <asp:TextBox ID="txtFechaEnvio" runat="server" CssClass="cajaTexto" Enabled="False" TextMode="Date"></asp:TextBox>
                        </td>
                        <th style="width: 190px;">
                            <asp:Button ID="BtnRegistrar" runat="server" Text="Registrar Recepción de Activo" CssClass="btn btn-primary" OnClick="BtnRegistrar_Click" />
                        </th>
                        <td>
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="BtnImprimir" runat="server" Text="Imprimir" Visible="true" Enabled="false" CssClass="btn btn-primary" OnClick="BtnImprimir_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gvRepara" runat="server" AutoGenerateColumns="False" BackColor="White" 
                                    BorderColor="#CCCCCC" BorderStyle="None" Font-Size="14px" BorderWidth="1px" CellPadding="6" Style="font-family: Calibri;" 
                                       Width="80%" CssClass="alignCenter" HorizontalAlign="Center">
                                    <%--<AlternatingRowStyle BackColor="#CCCCCC" />--%>
                                    <Columns>
                                        <asp:BoundField DataField="Codigo" HeaderText="Código">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción">
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Detalle" HeaderText="Detalle">
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>                                        
                                        <asp:TemplateField HeaderText="Costo">
                                            <HeaderStyle Width="70px" />
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtCosto" runat="server" Text='0.00' CssClass="alignCenter" Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Motivo">
                                            <HeaderStyle Width="220px" />
                                             <ItemTemplate>
                                                <asp:TextBox ID="txtMotivo" runat="server" Text='' CssClass="alignCenter" Width="250px" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="220px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Trabajo Realizado">
                                            <HeaderStyle Width="230px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTrabajo" runat="server" Text='' CssClass="alignCenter" Width="260px" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="230px" />
                                        </asp:TemplateField>
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

                    <asp:GridView ID="gvDetalle" runat="server" Visible="false"></asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
    <div>   
    </div>
    </form>
</body>
</html>
