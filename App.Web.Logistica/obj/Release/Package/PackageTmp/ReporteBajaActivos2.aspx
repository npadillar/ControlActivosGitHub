<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteBajaActivos2.aspx.cs" Inherits="App.Web.Logistica.Reportes.ReporteBajaActivos2" %>

<%@ Import Namespace="Logistica.Libreria.Negocio" %>
<%@ Import Namespace="Logistica.Libreria.Entidad" %>
<%@ Import Namespace="System.Collections.Generic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de bajas</title>

    <link rel='shortcut icon' type='image/x-icon' href="Iconos/sise.ico" />
    <link rel="Stylesheet" href="Scripts/estilos.css" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //agregar encabezado
            $('#gvBajaActivos tbody tr:first').wrap('<thead/>').parent().prependTo('#gvBajaActivos');

            $("#gvBajaActivos tbody").on('click', 'tr', function () {
                $("#gvBajaActivos tbody tr").removeClass("resaltar");
                $(this).addClass("resaltar");
            });
        });
    </script>
    <style type="text/css">
        .resaltar {
            background: #acd5f7;
        }

        #gvBajaActivos thead, tbody tr, tfoot tr {
            display: table;
            width: 100%;
            /*el ancho de cada columna permanecerá fijo, del mismo ancho que las columnas del cuerpo*/
            table-layout: fixed;
        }

        #gvBajaActivos thead {
            /*el ancho de la fila del encabezado sera todo menos el ancho del scroll vertical*/
            width: calc( 99.9% - 1em );
            text-align: center;
        }

        #gvBajaActivos {
            width: 99%;
        }

            #gvBajaActivos tbody {
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

            #gvBajaActivos tr td {
                /*ajustar el contenido al ancho de la columna del cuerpo*/
                word-wrap: break-word;
            }

            #gvBajaActivos thead tr th {
                /*ajustar el contenido al ancho de cada columna del encabezado*/
                word-wrap: break-word;
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
            <h5 class="alignCenter"><b>REPORTE DE BAJA DE ACTIVOS</b></h5>

            <table style="width: 950px;" class="tb">
                <tr>
                    <th colspan="4">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn btn-primary" />
                &nbsp;
                    <asp:Button ID="btnExportar" runat="server" OnClick="btnExportar_Click" Text="Exportar a excel" Visible="false" CssClass="btn btn-primary" />
                    </th>
                </tr>
                <tr>
                    <th style="width: 200px;">Número de baja:
                    </th>
                    <td>
                        <asp:TextBox ID="txtIdBaja" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th style="width: 120px;">Usuario:
                    </th>
                    <td>
                        <asp:DropDownList ID="cboUsuario" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                            <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th style="width: 200px;">Fecha desde:
                    </th>
                    <td>
                        <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th style="width: 120px;">Fecha hasta:
                    </th>
                    <td>
                        <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="cajaTexto"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <th style="width: 200px;">Motivo Baja Activo:
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlMotivoBaja" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                            <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th style="width: 120px;">Código:
                    </th>
                    <td>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 200px;">Disposicion Baja Activo:
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlDisposicion" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                            <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th style="width: 120px;">Serie:
                    </th>
                    <td>
                        <asp:TextBox ID="txtSerie" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 200px;">Marca:
                    </th>
                    <td>
                        <asp:TextBox ID="txtMarca" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                    <th style="width: 120px;">Modelo:
                    </th>
                    <td>
                        <asp:TextBox ID="txtModelo" runat="server" CssClass="cajaTexto"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>


        <div style="width: 90%; margin: 0 auto;">
            <asp:GridView ID="gvBajaActivos" DataKeyNames="Id" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" Font-Size="14px"
                BorderWidth="1px" CellPadding="4" Style="font-family: Calibri;">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Nro Baja">
                        <HeaderStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Usuario" HeaderText="Usuario">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaDocumento" HeaderText="Fecha Doc">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FechaBaja" HeaderText="Fecha Baja">
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaReg" HeaderText="Fecha Reg">
                        <HeaderStyle Width="110px" />
                        <ItemStyle Width="110px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Descripcion" HeaderText="Motivo">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Descrip" HeaderText="Disposicion">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LDescripcion" HeaderText="Descripcion">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Serie" HeaderText="Serie">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Marca" HeaderText="Marca">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Modelo" HeaderText="Modelo">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Sede" HeaderText="Sede">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
