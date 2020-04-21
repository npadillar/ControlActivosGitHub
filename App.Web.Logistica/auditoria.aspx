<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="auditoria.aspx.cs" Inherits="App.Web.Logistica.auditoria1" %>
<%@ Import Namespace="Logistica.Libreria.Negocio" %>
<%@ Import Namespace="Logistica.Libreria.Entidad" %>
<%@ Import Namespace="System.Collections.Generic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Auditoría</title>
    <link rel='shortcut icon' type='image/x-icon' href="Iconos/sise.ico" />
    <link rel="Stylesheet" href="Scripts/estilos.css" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //agregar encabezado
            $('#dgvDatos tbody tr:first').wrap('<thead/>').parent().prependTo('#dgvDatos');

            $("#dgvDatos tbody").on('click', 'tr', function () {
                $("#dgvDatos tbody tr").removeClass("resaltar");
                $(this).addClass("resaltar");
            });
        });
    </script>
    <style type="text/css">
        .resaltar {
            background: #acd5f7;
        }

        #dgvDatos thead, tbody tr, tfoot tr {
            display: table;
            width: 100%;
            /*el ancho de cada columna permanecerá fijo, del mismo ancho que las columnas del cuerpo*/
            table-layout: fixed;
        }

        #dgvDatos thead {
            /*el ancho de la fila del encabezado sera todo menos el ancho del scroll vertical*/
            /*width: calc( 99.9% - 1em );*/
            width: 1000px;
        }

        #dgvDatos {
            width: 1250px;
        }

            #dgvDatos tbody {
                /*max-height: 50px;*/
                /*mostrar el cuerpo*/
                display: block;
                /*altura del cuerpo*/
                height: 600px;
                /*overflow:auto;*/
                /*mostrar la barra vertical del cuerpo*/
                overflow-y: auto;
                /*ocultar la barra horizontal del cuerpo*/
                overflow-x: hidden;
                text-align: center;
            }

            #dgvDatos tr td {
                /*ajustar el contenido al ancho de la columna del cuerpo*/
                word-wrap: break-word;
            }

            #dgvDatos thead tr th {
                /*ajustar el contenido al ancho de cada columna del encabezado*/
                word-wrap: break-word;
            }

            #dgvDatos thead {
                /*color para el encabezado*/
                background-color: Silver;
            }

            #dgvDatos tfoot {
                /*color para el pie*/
                background-color: Silver;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
        </div>


        <div class="contentPrincipal">
            <h5 class="alignCenter"><b>REPORTE DE AUDITORÍA - HELLO222</b></h5>

            <table style="width: 950px;" class="tb">
                            <tr>
                                <td colspan="4">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn btn-primary" />
                                        &nbsp;
                                        <asp:Button ID="btnExportar" runat="server" Text="Exportar a excel" OnClick="btnExportar_Click" CssClass="btn btn-primary" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 120px;">
                                    Fecha desde:
                                </th>
                                <td>
                                    <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="cajaTexto"></asp:TextBox>
                                </td>
                                <th style="width: 120px;">
                                    Fecha hasta:
                                </th>
                                <td>
                                    <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="cajaTexto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 120px;">
                                    Acción:
                                </th>
                                <td>
                                    <asp:DropDownList ID="cboAccion" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                        <asp:ListItem Value="todos"> --- Todos --- </asp:ListItem>
                                        <asp:ListItem>Registro</asp:ListItem>
                                        <asp:ListItem>Modificación</asp:ListItem>
                                        <asp:ListItem>Recepción</asp:ListItem>
                                        <asp:ListItem>Baja</asp:ListItem>
                                        <asp:ListItem>Anulación</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th style="width: 120px;">
                                    Usuario:
                                </th>
                                <td>
                                    <asp:DropDownList ID="cboUsuario" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                        <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 120px;">
                                    Módulo:
                                </th>
                                <td>
                                    <asp:DropDownList ID="cboModulo" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                        <asp:ListItem Value="todos"> --- Todos --- </asp:ListItem>
                                        <asp:ListItem Value="Baja Activos">Baja Activos</asp:ListItem>
                                        <asp:ListItem Value="Guía de Activos">Guía de Activos</asp:ListItem>
                                        <asp:ListItem Value="Guía de Bienes">Guía de Bienes</asp:ListItem>
                                        <asp:ListItem Value="Mantenimiento Activos">Mantenimiento Activos</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th style="width: 120px;">
                                    Ingrese ID:
                                </th>
                                <td>
                                    <asp:TextBox ID="txtIdTabla" runat="server" CssClass="cajaTexto"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 120px;">
                                    Ingrese:
                                </th>
                                <td>
                                    <input id="txtCodigo" runat="server" type="text" class="cajaTexto" style="width: 450px;"
                                        placeholder="Código de activo / Número de documento / Número de baja" />
                                </td>
                            </tr>
                        </table>

            <div style="text-align: center;">
                            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
        </div>

                        <p style="width: 80%; margin: 0px auto; font-weight: bold;">
                            Total de registros encontrados: 
                            <asp:Label ID="lblRegistros" runat="server" Text="0"></asp:Label>
                        </p>
                        <asp:GridView ID="dgvDatos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" Font-Size="14px"
                            BorderWidth="1px" CellPadding="4" Style="font-family: Calibri; margin: 0 auto; text-align: center;">
                            <Columns>
                                <asp:BoundField DataField="idTabla" HeaderText="ID">
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigo" HeaderText="Código/Número">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="accion" HeaderText="Acción">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pagina" HeaderText="Módulo">
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="campo" HeaderText="Campo">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="val_anterior" HeaderText="Valor anterior">
                                    <HeaderStyle Width="200px" />
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="val_nuevo" HeaderText="Valor nuevo">
                                    <HeaderStyle Width="200px" />
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="hora" HeaderText="Hora">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ip" HeaderText="IP">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
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

    </form>
</body>
</html>
