   M S B A M L   `   `   `  ����P  LTaller_Escritorio_wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Taller_Escritorio_wpfTaller_Escritorio_wpf  5   	   &     Taller_Escritorio_wpf.MainWindow   5   	   V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Taller_Escritorio_wpf     5   	     � Title$  Login��5	   
   $	��600��6   $	��400��6%    � WindowStyle$
 None=�61    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�6^   .��5
      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $	��100 �6   �� 5      $��* �6   .+�5   
   �� -   5        $��0��5      
������ ._�5      � .+�5      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Accent=�6+   ���	#FF004445.��5      �� $��Right=�6!   .+�5      �� -   5        $��30��5   !   $��30��6,   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�68   $��0q�6i    J� OpacityMask
 ��������	#FF609152���	#FF004445
�������.��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ WindowClose=�66   6K   5      5      5      � 5      $��2��6   .+�5      �� -   5        	 ��Name$	 Btn_minimizar��5      $ Depth3=�66   $��30��6h   $��30��6t   $��334,0,36,0.4q�6   $��0q�6�   
 ��������	#FF609152���	#FF004445
�������.��5        $ WindowMinimize=�62   5      5      5       5!      � 5#   
   $��1��6   
 � ColumnDefinitions
 5$      �� 5%      $	��40* �6#   �� 5&      $
��261* �6#   �� 5'      $	��99* �6#   .+�5)        $ Accent=�6'   
���   �
 �   �
���   ����	#FF2C7873$��3��6�   $	��100��6�   .��5*      �� $��Bienvenido!��6   $��35(�6/   $��Bottom=�6=   $��
30 50 0 10q�6X   
�������$
��Bold"�6    J� Effect 5+      3  -System.Windows.Media.Effects.DropShadowEffect  5,      5.      5/      50      � 52   
   $��2��6   .+�53      ��  �� Orientation$ Vertical=�6   $��35,84,48,95q�60   .+�55      �� $ 
Horizontal=�6   $��46,0,40.6,0q�66   $��52��6K   .+�56        $ Account=�6.   $	��Top=�6y   $��0,30,0,0q�6�   $��22��6�   $��22��6�   ��6=   �� *BorderBrush+ �� ElementName$ txt_usuario��6�   �� 57      -   5         ��Name$ txt_usuario��57      $	��198��63   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  Usuario��6?      
IsFloating .  �� 
CaretBrush �	#FF609152 �� SelectionBrush �	#FF609152$��0,10,0,-0.2q�6�   58      �� 59      $ 
Horizontal=�6   $��46,40,40.6,0q�66   $��58��6L   .+�5:        $	 Key=�6.   $��Bottom=�6x   $��0,0,0,4q�6�   $��42��6�   $��21��6�   ��69   �� *BorderBrush+$ txt_contraseña��6�   G� 5;      -   5         G�Name$ txt_contraseña��5;   "   $	��198��6:   $  Contraseña��6F    .  G� 
CaretBrush �	#FF609152 G� SelectionBrush �	#FF609152$��40��6�   $	��Top=�6�   5<      �� 5=      -   5        $	 BTN_iniciar��5=       $   MaterialDesignRaisedAccentButton#���   6.   $��Iniciar��6j   $	��219��6|   $��Right=�6�   $��0,20,40.6,0q�6�   $
��Bold"�6�   ���	#FF6FB98F$��18(�6�   ���	#DDFFF8F8$��42��6!  5F      5G      � 5H   
   $
��Left=�6   $��31��6+   $��0,438.6,0,0q�67   $	��Top=�6L   $	��400��6d   $��2��6p   .+�5I      �� $%��  Taller Mecánico ServiExpress��6   $��15(�6>   $��Bottom=�6L   ���	#FF858983$
��Bold"�6   ���	#FFFFD800$��21��6�   $��0,0,0,9q�6�   5J      5K      5L      