   M S B A M L   `   `   `  ����H  DDurakWPF_Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DurakWPF_GameDurakWPF_Game  5   	       DurakWPF_Game.GameRules   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:DurakWPF_Game     5   	     � Title$  	GameRules��5   	   $	��250��6   $	��600��6(    � WindowStartupLocation$ CenterScreen=�64    � 
ResizeMode$ NoResize=�6Y   .��5	      � .+�5
   
   �� $
��Fill=�6    J� Opacity$	 0.5Q�6   $5��//Assets/Card Asset/Backgrounds/background_2.png��6-   $��2��6f   $��2��6z   �� 5   
   $
��Left=�6   $	��167��6/   $��	21,44,0,0q�6<   $
��Wrap=�6O   $	��Top=�6c   $	��547��6{   $��2��6�   $��2��6�   �� 5   
   .@�5      �� $��	Game Play��6   .��5      � ���	#FFE5E5E5.+�5       � -   5        $��GameRulesFrame1��5      $����This game uses a 36 standard deck cards. The game begins by the application randomly choosing the trump card 
and dealing 6 cards to player and opponent.
 
Attacking and Defending: Players take turns attacking and defending. 
The attacker plays a card face-up, and opponent must defend against it by playing 
a higher-ranking card of the same suit or any card of the trump suit. 

Winning: The first player to finish their cards wins!��64   $��2��6  5      5      �� 5      $��Controls��6   .��5      � ���	#FFE5E5E5.+�5       � $����Reset Game: Starts the game from the beginning.

Deal Card: Deal one card from the deck

Skip turn: Skip player's turn and end round. If player is attacking. the player will now defend, and vice versa.  
��6   5      5      5      5      5      