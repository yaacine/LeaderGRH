   M S B A M L   `   `   `  ����J  FWpfApplication2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication2WpfApplication2  5   	   &     WpfApplication2.DetailsEntretien   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication2     5   	   V RMaterialDesignThemes.Wpf, Version=2.3.2.1045, Culture=neutral, PublicKeyToken=nullX TMaterialDesignThemes.MahApps, Version=0.0.11.0, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5	   	     � Title$  modifierConges��5   	   $	��600��6    $	��600��6-      MaterialDesignBody#���   5
   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Icon$/ )photos/images freepik/logo grh leader.png��5   
   .��5      � .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��22q�6   $��10,10,47.5,0q�6+   $	��100��6A    J� RenderTransformOrigin$ 0.5,0.5&�6N   .��5      ��  �� TextAlignment$ Center=�6   $��40(�6/      MaterialDesignTitleTextBlock#���  6>   $��Detailes de l'entretien��6u   $��	0,0,0,-19q�6�   5      �� 5   
   $#��photos/detaille_entretien.png��6   $��75��67   $	��Top=�6C   $��	0,25,10,0q�6[   $��83.333��6n   $��Right=�6}     5   
   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�6   
�������$��Stretch=�6b   $��35,65,35,35.667q�6~   .��5      �� .��5      �� .+�5      ]� $
��4,10q�6   .+�5      �� $��nomero de l'entretien :��6    $��0,0,10,0q�6@   $	��141��6R   $
��Bold"�6^   6r   �� 5      -   5        $��numEntretien��5       $��k��64   $��224.333��6=   6O   5      �� 5       
���   �$��2��6/   $��0,0,0,5q�6:   $
��Bold"�6K   6_   ]� 5!      $
��4,10q�6   .+�5"      �� $��Date de l'entretient :��6    $��0,0,10,0q�6>   $
��Bold"�6P   $	��141��6b   6q   �� 5#      -   5        $��DateEntretien��5#       $��k��65   $��224.333��6>   6P   5$      �� 5%      
���   �$��2��63   $��0,0,0,5q�6>   $
��Bold"�6O   6c   ]� 5&      $
��4,10q�6   .+�5'      �� $��nomero du candidat :��6    $��0,0,10,0q�6=   $
��Bold"�6O   $	��141��6a   6p   �� 5(      -   5        $��numCandidat��5(       $��k��63   $��224.333��6<   6N   5)      �� 5*      
���   �$��2��63   $��0,0,0,5q�6>   $
��Bold"�6O   6c   ]� 5+      $
��4,10q�6   .+�5,      �� $��Nom du candidat :��6    $��0,0,10,0q�69   $
��Bold"�6K   $	��131��6]   6m   �� 5-      -   5        $��NomCandidat��5-       $��k��63   $��214.333��6<   6N   5.      �� 5/      
���   �$��2��63   $��0,0,0,5q�6>   $
��Bold"�6O   6c   ]� 50      $
��4,10q�6   .+�51      �� $��Prenom du candidat :��6    $��0,0,10,0q�6=   $
��Bold"�6O   6d   �� 52      -   5        $��PrenomCandidat��52       $��k��66   $��214.333��6?   6Q   53      �� 54      
���   �$��2��63   $��0,0,0,5q�6>   $
��Bold"�6O   6c   ]� 55      $
��4,10q�6   .+�56      �� $��Experience :��6    $��0,0,10,0q�66   $
��Bold"�6H   6\   �� 57      -   5        $��
experience��57       $��k��62   $��158.028��6;   6M   58      �� 59      
���   �$��2��63   $��0,0,0,5q�6>   $
��Bold"�6O   6c   ]� 5:      $
��4,10q�6   .+�5;      �� $��Question Numero 1 :��6    $��0,0,10,0q�6=   $
��Bold"�6O   6c   �� 5<      -   5         ��Name$ 	question1��5<       $��k��63   $��209.151��6<   6N   5=      �� 5>      
���   �$��2��63   $��0,0,0,5q�6>   $
��Bold"�6O   6c   ]� 5?      $��4,10,4,4q�6   .+�5@      �� $��Reponse 1 :��6    $��0,0,10,0q�65   $
��Bold"�6G   6[   �� 5A      -   5        $ reponse1��5A       $��k��62   $��151.239��6;   6M   5B      �� 5C      
���   �$��2��63   $��0,0,0,5q�6>   $
��Bold"�6O   6c   ]� 5D      $
��4,10q�6   .+�5E      �� $��Question Numero 2 :��6    $��0,0,10,0q�6=   $
��Bold"�6O   6c   �� 5F      -	   5        $ 	question2��5F       $��k��63   $��209.151��6<   6N   5G      �� 5H      
���   �$��2��63   $��0,0,0,5q�6>   $
��Bold"�6O   6c   ]� 5I      $��4,10,4,4q�6   .+�5J      �� $��Reponse 2 :��6    $��0,0,10,0q�65   $
��Bold"�6G   6[   �� 5K      -
   5        $ reponse2��5K       $��k��62   $��151.239��6;   6M   5L      5M      5N      5O      �� 5P   
   -   5         ��Name$ button��5P      $��Retour��6!   $��240,0,240,23.867q�62   $��Bottom=�6L   6g   5Q      5R      