Author: Jimmy Yuan

Date: Feb 22 2021

Sprint Number:2

Name of the .cs file being reviewed: KeyboardController.cs

Author of the .cs file being reviewed: Daniel Neff

Number of minutes taken to complete the review: 20

For code's Readability: code is commented well. Comments for the two methods are very detailed. Everything is pretty straightforward.
A dictionary stores both of the keys and their corresponding commands with ICommand interface. Commands are executed if the corresponding 
keys are found in the dictionary. The code is very readable overall.

For codes' Maintainability: low coupling because command pattern is used. The code is very easy to maintain with the RegisterCommand method
which can add more keys and their corresponding commands to the dictionary in the future. Even if new keys and commands are added, Update 
method does not need any changes. The code is very maintanable overall.

A hypothetical change: if the project needs to add more keys and commands in the future, the code will easily handle it due to the 
RegisterCommand option! Simply use the RegisterCommand method can add more keys and commands to the dictionary.
