   M S B A M L   `   `   `  ����S  OMicrosoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=nulleHclr-namespace:Microsoft.Kinect.Toolkit;assembly=Microsoft.Kinect.ToolkitMicrosoft.Kinect.Toolkit  5   	    
PhotoBooth7   1Hpi.Hci.Bachelorproject1617.PhotoBooth.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   VkHclr-namespace:Microsoft.Kinect.Toolkit;assembly=Microsoft.Kinect.Toolkit     5   	   -   5          � Title$  Linepod Photo Booth��5   	    � WindowStartupLocation$ CenterScreen=�6%   $
��1324��5      $	��750��6    � WindowState$ 	Maximized=�67    � 
ResizeMode$ NoResize=�6O    � WindowStyle$
 None=�6f    � 	Resources 5         MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush �� 
TargetType �� Property	 �� Value  SnapshotButton
 �� 
TargetType �� Orientation ��Name �� TextAlignment ��Name �� Triggers P� Property P� Value �� 
TargetName  SquareCheckBox �Name��%w
  &	        &	 #     &	 F     '�� i     &	 �     7  7  &	 �    7  7  �� 5	   
   $ 	#ff6e6e6e��62   �� 5
   
   $ 	#ff52318f��64   �� 5   
   $ 	#ff00BCF2��62   �� 5   
   " ��6   .�5      ��  �@J�SnapsToDevicePixels	 . 64   5      �� 5   
   " ��6   .�5      ��  � ��	 5      �� 5      "
 ��6&   .R�5      � .+�5      �� $ 
Horizontal=�6)   
������ .+�5   "   �� $ SnapText��6,   #��z��6>   $
 Left=�6_   $��Center=�6u   8��  $��15(�6�   � 5   "   $��9,0,0,0q�6'   .+�5   &   �� $ 
SnapNormal��6,   $��Images\SnapNormal.png��6@   $
��None=�6_   $��Center=�6n   �� 5   &   $ 	SnapHover��6,   $��Images\SnapHover.png��6?   $
��None=�6]   $��Center=�6l   $y�	Collapsed=�6�   5   #   5      5       5      P� 5       �@J�IsMouseOver . . �5   "   �� $ 
SnapNormal��6Q    � y�$	 	Collapsed=�6?   6Q   �� 5   "   $ 	SnapHover��6O    � y�$	 Visible=�6?   6O   �� 5    "   8	  $ SnapText��6h    � ��5!      5#      5%      5&      �� 5(   
   " ��6   .�5)      ��  � ��	 5*      �� 5+      "
 ��6&   .R�5,      � .+�5-      �� $ 
Horizontal=�6)   
������ .+�5.   "   � $ SquareCheckBoxChecked��6'   .+�5/   &   �� $ CheckedNormal��6,   $��Images\CheckedNormal.png��6C   $
��None=�6e   $��Center=�6t   �� 50   &   $ CheckedHover��6,   $��Images\CheckedHover.png��6B   $
��None=�6c   $��Center=�6r   $y�	Collapsed=�6�   51   #   � 52   "   $ SquareCheckBoxUnchecked��6'   $y�	Collapsed=�6H   .+�53   &   �� $ UncheckedNormal��6,   $ ��Images\UncheckedNormal.png��6E   $
��None=�6i   $��Center=�6x   �� 54   &   $ UncheckedHover��6,   $��Images\UncheckedHover.png��6D   $
��None=�6g   $��Center=�6v   $y�	Collapsed=�6�   55   #   �� 56   "   $ SquareCheckBoxText��6,   #��z��6H   $
 Left=�6i   $��Center=�6   8��  $��15(�6�   $��9,0,0,0q�6�   57      58       59      P� 5:       �@d�	IsChecked$ falseY�6;   . �5;   "   �� $ SquareCheckBoxChecked��6Q    � y�$	 	Collapsed=�6?   6Q   �� 5<   "   $ SquareCheckBoxUnchecked��6O    � y�$	 Visible=�6?   6O   5=      P� 5>       �@J�IsMouseOver . . �5?   "   �� $ CheckedNormal��6Q    � y�$	 	Collapsed=�6?   6Q   �� 5@   "   $ UncheckedNormal��6Q    � y�$	 	Collapsed=�6?   6Q   �� 5A   "   $ CheckedHover��6O    � y�$	 Visible=�6?   6O   �� 5B   "   $ UncheckedHover��6O    � y�$	 Visible=�6?   6O   �� 5C   "   8	  $ SquareCheckBoxText��6h    � ��5D      5F      5H      5I      5J      .��5L      � -   5        $��
layoutGrid��5L      $��	10 0 10 0q�6    � ColumnDefinitions 5M   
   �� 5N      $��0* �6   �� 5O      $	��58* �6   �� 5P      $
��313* �6    � RowDefinitions 5R   
   �� 5S      $
��Auto �6   �� 5T      �� 5U      $��50 �6   �� 5V      $��0 �6   �� 5W      $��50 �6   .+�5Y   
   � $��0��6   $��0 0 0 20q�6   $��3��6.    5Z      �� 5[      $
��130* �6#   �� 5\      $
��117* �6#   �� 5]      $
��248* �6#   �� 5^      $
��247* �6#   .+�5`      �� $��0��6   $��Images\Logo.png��6$   $
��Fill=�6=   $��32��6L   $��81��6X   $
��Left=�6c   $��0 10 0 5q�6~   �� 5a      $��3��6   $��0,0,-2,0q�6(   $��Right=�6:   $��Bottom=�6V   #��[  6q   $��Segoe UI)�6�   $��18(�6�   Linepod Photo Booth6�   4   .Microsoft.Kinect.Toolkit.KinectSensorChooserUI  5b      -   5        $��sensorChooserUi��5b   z   $��2��6&   $��Center=�66   $��104,0q�6S   $	��Top=�6b   6z   5c      � 5d   
   $��1��6   $��3��6    5e      �� 5f      $
��185* �6#   �� 5g      $
��185* �6#   .+�5i      �� -   5        $��Backdrop��5i      $��Images\empty.jpg��6%   $��0��6?   �� 5j      -   5        $��MaskedColor��5j      $��0��6(   (� 5k       (� Stretch$ Uniform=�6   $��Center=�6(   $��1��6E   .��5l      �� -   5        $��Image��5l      $	��640��6%   $	��480��61   5m      5n      �� 5o   
   -   5        $��buttonPrintSkel��5o   �   $��2��6   #��[ 6   $��Skeleton    ��6F   $��32��6]   $��Right=�6i   $��Center=�6�   $��
0,7,306,11q�6�   $��2��6�   $	��136��6�   �� 5p   
   -   5        $��buttonPrintOutlines��5p   �   $��2��6   #��[ 6   $��Outlines    ��6F   $��32��6]   $��Right=�6i   $��Center=�6�   $��
0,7,160,11q�6�   $��2��6�   $��130                 ��6  �� 5r   
   -	   5        $��buttonPrintBoth��5r   �   $��2��6   #��[ 6   $��Print         ��6F   $��32��6_   $��Right=�6k   $��Center=�6�   $��	0,7,10,11q�6�   $��2��6�   $��130                 ��6�   5z      5{      