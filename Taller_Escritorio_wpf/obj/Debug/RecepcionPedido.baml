   M S B A M L   `   `   `  ����P  LTaller_Escritorio_wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Taller_Escritorio_wpfTaller_Escritorio_wpf  5   	   +    %Taller_Escritorio_wpf.RecepcionPedido   5   	   V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Taller_Escritorio_wpf     5   	     � Title$  RecepcionPedido��5	   	   $	��800��6!   $
��1200��6.    � WindowStyle$
 None=�6;    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�6h   .��5
      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��70 �6   �� 5      $��* �6   .+�5   
   �� -   5        $��0��5      
������ ._�5      � .+�5      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Accent=�6+   ���	#FF004445.��5      �� $��Right=�6!   .+�5      �� -   5        $��30��5   !   $��30��6,   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�68   $��0q�6i    J� OpacityMask
 ��������	#FF609152���	#FF004445
�������.��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ WindowClose=�66   6K   5      5      5      � 5      $��2��6   .+�5      �� -   5        	 ��Name$	 Btn_minimizar��5      $ Depth3=�66   $��30��6h   $��30��6t   $��1136,0,34,0.4q�6   $��0q�6�   
 ��������	#FF609152���	#FF004445
�������.��5        $ WindowMinimize=�62   5      5      5      5       � 5!   
   $��1��6   
 � ColumnDefinitions
 5"      �� 5#      $	��40* �6#   �� 5$      $
��261* �6#   �� 5%      $	��99* �6#   .+�5'        $ Accent=�6'   
���   �
 �   �
���   ����	#FF2C7873$��3��6�   $	��100��6�   .��5(      �� $��35(�6   $��Bottom=�6*   $��
30 20 0 30q�6E   
�������$
��Bold"�6l   $��Recepcionar Pedido��6~    J� Effect 5)      3  -System.Windows.Media.Effects.DropShadowEffect  5*      5,      5-      5.      � 5/   
   $��2��6   .+�50      �� -   5        $	 	btn_MenuP��50       $   MaterialDesignRaisedAccentButton#���   6)   $��  Menú Principal��6|   $	��240��6�   $
��Auto��6�   $
��Bold"�6�   ���	#FFF0EAEA$��16(�6�   $��0,60,899,578q�6�   $��Right=�6  ��6   Z� 6    51      $
 Home=�6&   $��62��62   $��59��6>   $��	61,60,0,0q�6I    J� RenderTransformOrigin$ 1.244,0.753&�6\   52      � 53   
   $��2��6   .+�54      �� -   5        $	 Btn_pedidos��54      #���   6+   $��GENERAR PEDIDO��6g   $	��240��6�   $
��Auto��6�   $��Right=�6�   $
��Bold"�6�   ���	#FF6FB98F$��16(�6�   $��0,480,901,162q�6
    55      $ 	TruckFast=�6&   $��58��67   $��42��6C   $��
59,480,0,0q�6N   $ -0.133,-0.033&�6b   56      � 57   
   $��3��6   .+�58      �� -   5        $	 Btn_recep_p��58      #���   6+   $��       RECEPCIONAR PEDIDO��6�   $	��240��6�   $
��Auto��6�   $��Right=�6�   $
��Bold"�6�   ���	#FF6FB98F$��16(�6  $��0,559,900,80q�6    59      $ 
BasketFill=�6&   $��59��68   $��49��6D   $��
60,561,0,0q�6O   $ 16.58,-6.125&�6c   &   System.Windows.Controls.DataGrid  5:      -   5          Name$ DG_Recep��5:      $
��Left=�6)   $	��256��6D   $��343,281,0,0q�6Q   $	��Top=�6f   $	��814��6~      
IsReadOnly . $��10(�6�   $
��Bold"�6�   5;      �� 5<   
   -   5         ��Name$ txt_id_recep��5<      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  ID Recepción��6(      
IsFloating .  �� 
CaretBrush �	#FF609152 �� SelectionBrush �	#FF609152$
��Left=�6�   $��44��6�   $��421,35.2,0,0q�6�   $��2��6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6  $��78��6,  {�.  67  (  "System.Windows.Controls.DatePicker  5=   
   -	   5          Name$ txt_fecha_recep��5=      $  Fecha��6.    . $
��Left=�6�   $��44��6�   $��562,96.2,0,0q�6�   $��2��6�   $	��Top=�6�   $��97��6�   {�.  6�   �� 5>   
   -
   5        $ txt_id_pedido��5>      $ 
 ID Pedido��6)    .  �	#FF609152 �	#FF609152$
��Left=�6�   $��44��6�   $��421,96.2,0,0q�6�   $��2��6�   $
 Wrap=�6�   $	��Top=�6  $��78��6*  �� 5?   
   -   5         ��Name$ cmb_estado_recep��5?      $ Selecionar Estado��6-    . $
��Left=�6�   $��680,98.2,0,0q�6�   $	��Top=�6�   $	��161��6�   $��42��6�   $��2��6�   �� 5@   
   -   5        $ cmb_proveedor��5@      $ 	Proveedor��6*    . $
��Left=�6�   $��421,159.2,0,0q�6�   $	��Top=�6�   $	��380��6�   $��42��6�   $��2��6�   {�.  6�   �� 5A   
   -   5        $ cmb_empleado��5A      $ Selecionar Empleado��6)    . $
��Left=�6�   $��861,98.2,0,0q�6�   $	��Top=�6�   $	��161��6�   $��42��6�   $��2��6�   �� 5B   
   -   5        $ 
txt_IdProd��5B      $ ID Producto��6&    .  �	#FF609152 �	#FF609152$��361,218.2,760,440.8q�6�    �� 	IsEnabled . {�.  $��2��6�   �� 5D   
   -   5        $ txt_Sku_producto��5D      $  SKU Producto��6,    .  �	#FF609152 �	#FF609152$��462,218.2,598,440.8q�6�    . {�.  $��2��6  �� 5E   
   -   5        $ txt_descr_producto��5E      $ Descripción Producto��6.    .  �	#FF609152 �	#FF609152$��620,214.2,380,440.8q�6�    . {�.  $ 1.706,-0.662&�6  $��2��62    5F   
   -   5        $ DP_fecha_Venc��5F      $
��Left=�6,   $ Vencimiento��6G    . $��841,218.2,0,0q�6�   $	��Top=�6�   $��99��6�   $��41��6�   $��2��6�   �� 5H   
   -   5        $ txt_cantidad_recep��5H      $ 	 Cantidad��6.    .  �	#FF609152 �	#FF609152$
��Left=�6�   $��41��6�   $��960,218.2,0,0q�6�   $��2��6�   $
 Wrap=�6  $	��Top=�6  $��62��6/  �� 5I   
   -   5        $ txt_total_recep��5I      $  TOTAL��6+    .  �	#FF609152 �	#FF609152$
��Left=�6�   $��56��6�   $��343,539.2,0,0q�6�   $��2��6�   $
 Wrap=�6�   $	��Top=�6  $	��142��6*  $ 0.591,-0.583&�66  $��18(�6[  {�.  $
��Bold"�6{  � 5K   
   $��2��6   .+�5L        $ Search=�6&   $��33��64   $��33��6@   $��501,107,0,0q�6K   $ 11.861,-2.252&�6`   �� 5M      -   5        $	 Btn_Busca_Ped��5M      #���   6-   $�� ��6h   $��33��6s   $��33��6~   $
��Bold"�6�   $��16(�6�   $��0,107,666,560q�6�   $��Right=�6�   $ 2.715,0.306&�6(  ��6�   Z� ��6�   Z� 6(  5N      � 5O   
   $��2��6   .+�5P        $ DatabaseAdd=�6&   $��30��69   $��40��6E   $��1040,229,0,0q�6P   $ 11.125,0.168&�6f   ���	#FF51B06D6�   �� 5Q      -   5        $	 Btn_Confirmar_Prod��5Q      #���   62   $�� ��6m   $��40��6x   $
��Auto��6�   $
��Bold"�6�   $��16(�6�   $��0,229,120,441q�6�   $��Right=�6�   $ 
2.44,0.547&�6  ��6�   Z� ��6�   Z� 66  5R      �� 5S   
   $��71��6   $��914,539.2,45,0q�6!   $	��Top=�69   ���	#FFFAFDFF$ 
0.649,0.76&�6i   $��2��6�   .+�5T      �� -   5        $	 Btn_Confirmar_Rec��5T      $�� CONFIRMAR RECEPCIÓN��60   $��54��6O   $��
0,0,-0.2,0q�6[   ���	#FF5AC1CB���	#DDFFFFFF6�   5U      � 5X   
   $
��Left=�6   $��30��6+   $��0,660.2,0,0q�67   $	��Top=�6L   $
��1100��6d   $��2��6q   
 5Y      �� 5Z      $��* �6#   .+�5\      �� $%��  Taller Mecánico ServiExpress��6   $��18(�6>   $��Bottom=�6L   ���	#FF858983$
��Bold"�6   ���	#FFFFD800$��31��6�   $��
0,-1,-97,0q�6�   5]      5_      5`      