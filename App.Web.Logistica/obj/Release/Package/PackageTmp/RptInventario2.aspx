<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptInventario2.aspx.cs" Inherits="App.Web.Logistica.Reportes.RptInventario2" %>

<%@ Import Namespace="Logistica.Libreria.Negocio" %>
<%@ Import Namespace="Logistica.Libreria.Entidad" %>
<%@ Import Namespace="System.Collections.Generic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de activos</title>
    <link rel='shortcut icon' type='image/x-icon' href="Iconos/sise.ico" />
    <link rel="Stylesheet" href="Scripts/estilos.css" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //agregar encabezado
            $('#gdvDatos tbody tr:first').wrap('<thead/>').parent().prependTo('#gdvDatos');

            $("#gdvDatos tbody").on('click', 'tr', function () {
                $("#gdvDatos tbody tr").removeClass("resaltar");
                $(this).addClass("resaltar");
            });
        });
    </script>
    <style type="text/css">
        .resaltar {
            background: #acd5f7;
        }

        #gdvDatos thead, tbody tr, tfoot tr {
            display: table;
            width: 100%;
            /*el ancho de cada columna permanecerá fijo, del mismo ancho que las columnas del cuerpo*/
            table-layout: fixed;
        }

        #gdvDatos thead {
            /*el ancho de la fila del encabezado sera todo menos el ancho del scroll vertical*/
            width: calc(99.9% - 1em);
        }

        #gdvDatos {
            width: 99%;
        }

            #gdvDatos tbody {
                /*max-height: 50px;*/
                /*mostrar el cuerpo*/
                display: block;
                /*altura del cuerpo*/
                height: 400px;
                /*overflow:auto;*/
                /*mostrar la barra vertical del cuerpo*/
                overflow-y: auto;
                /*ocultar la barra horizontal del cuerpo*/
                overflow-x: hidden;
                text-align: center;
            }

            #gdvDatos tr td {
                /*ajustar el contenido al ancho de la columna del cuerpo*/
                word-wrap: break-word;
            }

            #gdvDatos thead tr th {
                /*ajustar el contenido al ancho de cada columna del encabezado*/
                word-wrap: break-word;
            }

            #gdvDatos thead {
                /*color para el encabezado*/
                background-color: Silver;
            }

            #gdvDatos tfoot {
                /*color para el pie*/
                background-color: Silver;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Pagina">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

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
                <h5 class="alignCenter"><b>REPORTE DE ACTIVOS</b></h5>

                <table style="width: 950px;" class="tb">
                    <tr>
                        <th style="width: 150px;">Código Barras:</th>
                        <td>
                            <asp:TextBox ID="txtCodigo" runat="server" MaxLength="100" CssClass="cajaTexto" />
                        </td>
                        <th style="width: 150px;">Edificio:</th>
                        <td>
                            <asp:TextBox ID="txtEdificio" runat="server" MaxLength="100" CssClass="cajaTexto" />
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">Descripción:</th>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100" CssClass="cajaTexto" />
                        </td>
                        <th style="width: 150px;">Categoria:</th>
                        <td>
                            <asp:DropDownList ID="ddlCategoria" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">Sede:</th>
                        <td>
                            <asp:DropDownList ID="ddlSede" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <th style="width: 150px;">Estado:</th>
                        <td>
                            <asp:DropDownList ID="ddlCondicion" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">Fecha desde:</th>
                        <td>
                            <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                        <th style="width: 150px;">Feha hasta:</th>
                        <td>
                            <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">Aula:</th>
                        <td>
                            <asp:TextBox ID="txtAula" runat="server" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                        <th style="width: 150px;">Area:</th>
                        <td>
                            <asp:TextBox ID="txtArea" runat="server" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">RUC:</th>
                        <td>
                            <asp:TextBox ID="txtRuc" runat="server" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                        <th style="width: 150px;">Nro Factura:</th>
                        <td>
                            <asp:TextBox ID="txtNroFactura" runat="server" CssClass="cajaTexto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 150px;">Usuario:</th>
                        <td>
                            <asp:DropDownList ID="ddlUsuario" runat="server" AppendDataBoundItems="true" CssClass="cajaTexto">
                                <asp:ListItem Value="0"> --- Todos --- </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <th style="width: 150px;">Código anterior:</th>
                        <td>
                            <asp:TextBox ID="txtCodAnterior" runat="server" MaxLength="20" CssClass="cajaTexto" /><br />
                            <asp:CheckBox ID="ckbMostrar" runat="server" Text="&nbsp;Mostrar columna" Style="font-family: Calibri;" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Font-Bold="true" OnClick="btnBuscar_Click" Text="Buscar" />
                            &nbsp;
                            <asp:Button ID="btnExportar" runat="server" CssClass="btn btn-primary" Font-Bold="true" OnClick="btnExportar_Click" Text="Exportar a PDF" />
                            &nbsp;
                            <asp:Button ID="btnExportarExcel" runat="server" CssClass="btn btn-primary" Font-Bold="true" Text="Exportar a excel" Visible="false" OnClick="btnExportarExcel_Click" />
                        </td>
                    </tr>
                </table>
            </div>


            <h6>Total de registros encontrados: 
                            <asp:Label ID="lblRegistros" runat="server" Text="0"></asp:Label>
            </h6>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="margin: 0 auto;">
                        <asp:GridView ID="gdvDatos" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" Font-Size="14px" BorderWidth="1px" CellPadding="4" Style="font-family: Calibri;">
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplEdit" runat="server" CssClass="imgEdit modalPopUp">
                        <img alt="" border="0" src="../App_Images/imgEdit.png" />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nro"><%--  <asp:BoundField HeaderText="Nro.">--%><%--    <HeaderStyle Width="50px" />--%><%--<ItemStyle Width="50px" HorizontalAlign="Center" />
                 </asp:BoundField>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="<%# (gdvDatos.PageSize * gdvDatos.PageIndex) + Container.DisplayIndex + 1%>">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaDoc" HeaderText="Fecha Doc.">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Codigo" HeaderText="Código Barras">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codAnterior" HeaderText="Código Anterior">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CATEGORIA" HeaderText="Categoria">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Usuario Asignado" HeaderText="Usuario Asignado">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SEDE" HeaderText="Sede">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EDIFICIO" HeaderText="Edificio">
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PISO" HeaderText="Piso">
                                    <HeaderStyle Width="25px" />
                                    <ItemStyle Width="25px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA" HeaderText="Área">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Aula" HeaderText="Aula">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Marca" HeaderText="Marca">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Modelo" HeaderText="Modelo">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Serie" HeaderText="Serie">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Condicion" HeaderText="Cond.">
                                    <HeaderStyle Width="45px" />
                                    <ItemStyle Width="45px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha_Registro" HeaderText="Fecha Reg.">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha Compra" HeaderText="Fecha Compra">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ruc" HeaderText="Ruc">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Numero Factura" HeaderText="Numero Factura">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Observacion" HeaderText="Observ.">
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:ButtonField CommandName="detalle" Text="Detalle" Visible="False" />
                                <asp:ButtonField CommandName="guia" Text="Guía" Visible="False" />
                                <asp:HyperLinkField DataNavigateUrlFields="codigo" DataNavigateUrlFormatString="detalleActivo.aspx?c={0}" Target="_blank" Text="Detalle">
                                    <HeaderStyle Width="45px" />
                                    <ItemStyle Width="45px" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="detalleGuiaActivo.aspx?c={0}" Target="_blank" Text="Guía">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle Width="35px" />
                                </asp:HyperLinkField>
                            </Columns>
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <HeaderStyle BackColor="#000000" Font-Bold="True" ForeColor="#FFFFFF" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
