   M S B A M L   `   `   `  ����N  Jintermediate agency, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:intermediate_agencyintermediate_agency  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5	   	   )    #intermediate_agency.EditOrderWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:intermediate_agency     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   Bcol2clr-namespace:System.Collections;assembly=mscorlib     5	   	   -   5        $��	TheWindow��5   T     � Title$  EditOrderWindow��6	    � SizeToContent$ Height=�6!   $	��800��68   $	��500��6H    � 	Resources 5      "  System.Collections.ArrayList   Statuses��%�   &	          5   
   ��5      NotAccepted��6&   ��5      
InProgress��6%   ��5      WaitingPayment��6)   ��5      	Fulfilled��6$   ��5      Canceled��6#   ��5      
Failed��6!   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   6-   �� 5      6!    � RowDefinitions 5    
   �� 5!      $
��Auto �6   6,   �� 5"      $
��Auto �6   6,   �� 5#      $
��Auto �6   6,   �� 5$      $
��Auto �6   6,   �� 5%      $
��Auto �6   6,   �� 5&      $
��Auto �6   6,   .+�5)   
   �� $��0��6   $��Center=�6!   Customer name6K   �� 5*   
   $��1��6   $��Center=�6!   Status6D   �� 5+   
   $��2��6   $��Center=�6!   	Manager6E   �� 5-   
   -   5        $��CustomerComboBox��5-      $��0��5.      $��1��6    ��5/      ��  �� Path$ CustomerList� �� SelectedItem 50      �� $ Order.Owner���51      �� 52      .R�53      �� ��6   �� $
 Name�54      56      �� 57   
   -   5        $��OrderStatusComboBox��57      $��1��6.   $��1��6;   #��[  58      59      �� 5:   
   $��2��6   $��1��6!   $��Center=�5;      $��Center=�5<      ��5=      �� $ Order.Manager.Name��� 5?   
   $��3��6   $��2��6!   $��Center=�65   Order details6a   &   System.Windows.Controls.DataGrid  5@   
   $��4��6   $��2��6       AutoGenerateColumns .  ��5B      �� $ Order.MerchOrders�   Columns 5C      4  .System.Windows.Controls.DataGridComboBoxColumn  5D      ,  &System.Windows.Controls.DataGridColumn	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 300 6)   
   Header$
 Merchandise��65      ItemsSource 5E   )   ��  �� ElementName$ 	TheWindow��$ 	MerchList�   SelectedItemBinding 5F   )   �� $ 	MerchType�   SelectedValueBinding 5G   )   �� $
 Name�0  *System.Windows.Controls.DataGridTextColumn  5H      $		 100 6%   $
 Amount��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� $ Amount�4  .System.Windows.Controls.DataGridCheckBoxColumn  5I      $		 100 6)   $
 	Complited��65    6H   �� $ IsComplited�5K      �� 5N   
   $��Center=�6    �� Orientation$ 
Horizontal=�62   $��5��6K   $��0��6X   $��2��6h   .+�5O      �� -   5         �� 	IsDefault . $��60��5O   ;   $��10q�6I   .��6U   OK6Y   �� 5P       �� IsCancel . $��60��6%   $��10q�63   .��6?   Отмена6G   5Q      5S      5T      