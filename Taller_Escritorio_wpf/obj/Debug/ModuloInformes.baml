   M S B A M L   `   `   `  ����P  LTaller_Escritorio_wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Taller_Escritorio_wpfTaller_Escritorio_wpf  5   	   *    $Taller_Escritorio_wpf.ModuloInformes   5   	   V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Taller_Escritorio_wpf     5   	     � Title$  ModuloInformes��5	   	   $	��800��6    $
��1200��6-    � WindowStyle$
 None=�6:    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�6g   .��5
      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��70 �6   �� 5      $��* �6   .+�5   
   �� -   5        $��0��5      
������ ._�5      � .+�5      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Accent=�6+   ���	#FF004445.��5      �� $��Right=�6!   .+�5      �� -   5        $��30��5   !   $��30��6,   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�68   $��0q�6i    J� OpacityMask
 ��������	#FF609152���	#FF004445
�������.��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ WindowClose=�66   6K   5      5      5      � 5      $��2��6   .+�5      �� -   5        	 ��Name$	 Btn_minimizar��5      $ Depth3=�66   $��30��6h   $��30��6t   $��1136,0,34,0.4q�6   $��0q�6�   
 ��������	#FF609152���	#FF004445
�������.��5        $ WindowMinimize=�62   5      5      5       5!      � 5"   
   $��1��6   
 � ColumnDefinitions
 5#      �� 5$      $	��40* �6#   �� 5%      $
��261* �6#   �� 5&      $	��99* �6#   .+�5(        $ Accent=�6'   
���   �
 �   �
���   ����	#FF2C7873$��3��6�   $	��100��6�   .��5)      �� $��35(�6   $��Bottom=�6*   $��	62,0,0,30q�6E   
�������$
��Bold"�6k   $��Modulo Informes��6}    J� Effect 5*      3  -System.Windows.Media.Effects.DropShadowEffect  5+      5-      5.      �� 5/      $
��Left=�6   $	��306��63   $��220,85,0,-320.6q�6@   ���	#FF232222$	��Top=�6l   $	��838��6�   $��1��6�   $��2��6�    50      -  'System.Windows.Media.Effects.BlurEffect  51      53      �� 54      -   5         ��Name$ label��54      $�� -��6#   $��2��60   $
��Left=�6@   $��16.8,127,0,-77.6q�6[   $	��Top=�6u    J� RenderTransformOrigin$ -0.458,6.637&�6�   $��21��6�   $��18��6�   �� 55      -   5        $ label1��55      $%��Informe de recepción de pedido��6$   $��1��6M   $
��Left=�6]   $��515,88,0,-52q�6x   $	��Top=�6�   $	��299��6�   $
��Bold"�6�   $��18(�6�   $��34��6�   ���	#FF2C7873$��2��6  ��6�   Z� 6  �� 56      -   5         ��Name$ cmb_Empl_info��56      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Empleado��6.      
IsFloating . $��261,111,403.2,-80.6q�6�   $��40��6�   $��1��6�   �� 57      -   5        $ cmb_prov_info��57      $ 	Proveedor��6.    . $��401,111,83.2,-80.6q�6�   $��40��6�   $��1��6�   (  "System.Windows.Controls.DatePicker  58      -   5          Name$ txt_Fecha_ini_info��58      $ Fecha Inicial��65   $��1��6d   $��720,122,280,-80.6q�6t   $��29��6�   $��2��6�     59      -	   5        $ txt_Fecha_fn_info��59      $ Fecha Final��64   $��2��6a   $��39.8,122,176,-80.6q�6q   $��29��6�   � 5:      $��2��6   .+�5;        $ Search=�6*   $��33��68   $��33��6D   $��1041,117,-954,-79.6q�6O   �� 5<      -
   5        $	 Btn_Busca_Ped_info��5<       $   MaterialDesignRaisedAccentButton#���   66   $�� ��6q   $��33��6|   $��33��6�   $
��Bold"�6�   $��16(�6�   $��0,117,-954,-79.6q�6�   $��Right=�6�   ��6�   Z� ��6�   Z� 6  5>      &   System.Windows.Controls.DataGrid  5@      -   5          Name$ Dt_G_list_pedido_info��5@      $
��Left=�66   $	��200��6Q   $��243,171,0,-300.6q�6^   $	��Top=�6x   $	��737��6�      
IsReadOnly . $��2��6�   $��1��6�   $��10(�6�   � 5A      $��2��6   .+�5B        $ MicrosoftExcel=�6*   $��40��6@   $��40��6L   $��1121,330,-1041,-299.6q�6W   ���	#FF1882496v   �� 5C      -   5        $	 Btn_Gene_Excel��5C      #���   62   $�� ��6m   $��40��6x   $
��Auto��6�   $
��Bold"�6�   $��16(�6�   $��0,330,-1041,-299.6q�6�   
���   �$��Right=�6�   ��6�   Z�  5D        5E      5G      5H      �� 5R      $
��Left=�6   $	��305��63   $��220,406,0,-640.6q�6@   ���	#FF232222$	��Top=�6m   $	��838��6�   $��1��6�   $��2��6�    5S        5T         Radius$ 4Q�6!   5V      �� 5W      -   5        $ label2��5W      $ ��Informe de venta productos��6$   $��1��6I   $
��Left=�6Y   $��515,406,0,-370q�6t   $	��Top=�6�   $	��299��6�   $
��Bold"�6�   $��18(�6�   $��34��6�   ���	#FF2C7873$��2��6	  ��6�   Z� 6	  �� 5X      -   5        $ cmb_Tipo_Doc_info_vta��5X      $ Tipo de Documento��66    . $��302,431,343,-401q�6�   $��40��6�   $��1��6�   �� 5Y      -   5        $ cmb_familiaProd_info_vta��5Y      $ Familia Producto��69    . $��462,431,104,-401q�6�   $��40��6�   $��1��6�     5Z      -   5        $ txt_Fecha_ini_Venta��5Z      $ Fecha Inicial��66   $��1��6e   $��720,442,280,-400.6q�6u   $��29��6�   $��2��6�   �� 5[      -   5        $ label4��5[      $�� -��6$   $��2��61   $
��Left=�6A   $��16.8,447,0,-400.6q�6\   $	��Top=�6w   $ -0.458,6.637&�6�   $��24��6�   $��18��6�     5\      -   5        $ txt_Fecha_Final_Venta��5\      $ Fecha Final��68   $��2��6e   $��39.8,442,176,-400.6q�6u   $��29��6�   � 5]      $��2��6   .+�5^        $ Search=�6*   $��34��68   $��33��6D   $��1041,436,-954,-400q�6O   �� 5_      -   5        $	 Btn_Busca_Vent_info��5_      #���   67   $�� ��6r   $��33��6}   $��33��6�   $
��Bold"�6�   $��16(�6�   $��0,436,-954,-400q�6�   $��Right=�6�   ��6�   Z� ��6�   Z� 6  5a      � 5b      $��2��6   .+�5c        $ MicrosoftExcel=�6*   $��40��6@   $��40��6L   $��1121,650,-1041,-619.6q�6W   ���	#FF1882496v   �� 5d      -   5        $	 Btn_Gene_Excel_Vta��5d      #���   66   $�� ��6q   $��40��6|   $
��Auto��6�   $
��Bold"�6�   $��16(�6�   $��0,650,-1041,-619.6q�6�   
���   �$��Right=�6�   ��6�   Z�  5e        5f      5h      5i        5j      -   5        $ Dt_G_list_venta��5j      $
��Left=�60   $	��201��6K   $��302,490,0,-620.6q�6X   $	��Top=�6r   $	��678��6�    . $��2��6�   $��1��6�   5n      � 5q   
   $��2��6   $��-10,0.2,10,0q�6   }�. 
 5r      �� 5s      $
��121* �6#   �� 5t      $
��479* �6#   .+�5w      �� -   5        $	 Btn_Menu��5w      #���   6(   $��  Menú Principal��6c   $	��236��6~   $
��Auto��6�   $
��Bold"�6�   ���	#FFF0EAEA$��16(�6�   $��0,60,891,580.8q�6�   $��Right=�6�   $��2��6  ��6�   Z� 6/    5x      $
 Home=�6&   $��58��62   $��56��6>   $��	73,60,0,0q�6I   $ 0.764,0.471&�6\   5z      �� 5|   
   $��58��6   $��60,561.2,901,0q�6!   $	��Top=�69   ���	#FFFFFCFA$ 
0.649,0.76&�6i   $��2��6�   .+�5}      �� -   5        $	 Btn_Mod_Informes��5}      $��MODULO INFORMES��6/   $��58��6I   ���	#FF6FB98F���	#DDFFFFFF$
��Bold"�6�   $��	0,0,0.2,0q�6�   5~      �� 5   
   $��60��6   $��60,480.2,901,0q�6!   $	��Top=�69   ���	#FFFFFCFA$ 0.494,0.541&�6i   $��2��6�   � 5�   
   $
��Left=�6   $��30��6+   $��0,660.2,0,0q�67   $	��Top=�6L   $
��1100��6d   $��2��6q   
 5�      �� 5�      $��* �6#   .+�5�      �� $%��  Taller Mecánico ServiExpress��6   $��18(�6>   $��Bottom=�6L   ���	#FF858983$
��Bold"�6   ���	#FFFFD800$��31��6�   $��
0,-1,-97,0q�6�   5�      5�      5�      