RecipeApp
Description
RecipeApp is a WPF-based application designed to help users manage their recipes efficiently. Users can create, view, scale, and manage recipes within the application. The primary goal of RecipeApp is to provide a user-friendly interface for organizing and manipulating recipe data.

Installation Instructions:
To set up and run RecipeApp, follow these steps:
- Ensure that you have .NET Core installed on your system.
- Clone the RecipeApp2 repository from GitHub to your local machine.
- Navigate to the directory containing the RecipeApp3 solution.
- Open the solution in your preferred IDE (e.g., Visual Studio, Visual Studio Code).
- Build the solution to restore dependencies and compile the code.
- Run the application from the IDE or command line.
  Usage Instructions:
- To use RecipeApp, follow these steps:
- Upon running the application, you will be presented with a menu of options.
- Choose the desired action from the menu, such as entering a new recipe, displaying all recipes, Displaying recipe details, scaling a recipe, resetting to original values and clearing data.
- Follow the prompts to input recipe details or select options from the menu.
- Interact with the application using the provided commands and prompts to manage recipes effectively.
 ![image](https://github.com/ST10262825/PROG6221-POE/assets/128587768/0e89ea4d-1aa9-4f0e-a083-6d1984df9455)


Class Structure
Recipe: Represents an individual recipe and contains properties for recipe details such as name, ingredients, quantities, and steps. It also includes methods for entering recipe details, displaying a recipe, scaling a recipe, and resetting quantities.
RecipeManager: Manages a collection of recipes and provides functionality for creating, viewing, scaling, and managing recipes. It interacts with the Recipe class to perform various operations on recipes.
Program: Contains the entry point of the application and orchestrates the interaction with the user interface.
InputUtility: Provides helper methods for validating and processing user input.
MainWindow: The main window of the application where users can navigate to different functionalities.
RecipeDetailsWindow: Displays detailed information for a selected recipe, including ingredients, quantities, steps, and total calories.
InputWindow: Used for entering new recipe details, including name, ingredients, quantities, and steps.
DisplayWindow: Shows a list of all existing recipes.
SelectWindow: Allows users to select a recipe from a list for viewing or editing.
ComboBoxWindow: Provides a user interface for selecting options from a combo box, used for various selections within the app.

Method Explanation:
- EnterNewRecipe: Allows users to enter details for a new recipe, including the recipe name, ingredients, quantities, and steps.
- DisplayAllRecipes: Displays a list of all existing recipes.
- DisplayRecipeDetails: Displays detailed information for a selected recipe, including ingredients, quantities, steps, and total calories.
- ScaleRecipe: Allows users to scale a selected recipe by a specified factor (e.g., double, triple).
- ResetQuantities: Resets the quantities of ingredients in a recipe to their original values.
- Recipe_OnHighCalorie: Event handler for notifying users when a recipe has a high calorie count.

Scaling Functionality:
- RecipeApp allows users to scale recipes by a specified factor. When scaling a recipe, the application adjusts the quantities of ingredients proportionally to the scaling factor. It supports scaling for measurements such as teaspoons, tablespoons, and cups. The program handles unit conversions automatically, ensuring that the scaled recipe maintains correct measurements.

Filtering Functionality
RecipeApp provides a feature to filter recipes by food group. Users can select a food group from a combo box, and the application will display recipes that belong to the selected food group. This feature helps users to quickly find recipes based on their dietary preferences or requirements.
Program Execution:
To execute RecipeApp, navigate to the directory containing the compiled application (e.g., .exe file) and run it from the command line. Alternatively, open the solution in an IDE and run the application from the integrated development environment.

Error Handling:
RecipeApp incorporates robust error handling mechanisms to ensure smooth user interactions and prevent unexpected behavior. The application employs several strategies to handle invalid inputs or unexpected conditions gracefully:
- Input Validation: Throughout the application, input validation is performed to ensure that user inputs meet specified criteria. For example:
When entering recipe details, the application validates inputs to ensure that strings are not empty and contain only alphabetic characters for recipe names, ingredient names, food group names, and steps.
Numeric inputs, such as quantities and calorie counts, are validated to ensure they are positive numbers.
- Error Messages: If input validation fails or an error occurs during execution, RecipeApp provides informative error messages to guide users in resolving issues. Error messages are displayed in red text to distinguish them from regular output. For instance:
When entering recipe details, users are prompted to re-enter inputs if they are invalid or empty.
- If a recipe with the same name already exists, users are notified to choose a unique name for the new recipe.
- Invalid Choice Handling: In menus and interactive prompts, RecipeApp handles invalid user choices gracefully by prompting users to re-enter valid options. For example:
When selecting a recipe from a list, users are prompted to choose a valid number corresponding to the desired recipe. If an invalid choice is entered, an error message is displayed, and users are prompted again.
- Exception Handling: RecipeApp utilizes exception handling to catch and handle runtime errors that may occur during program execution. Exceptions are logged or displayed to the user with appropriate error messages, allowing the application to recover gracefully from unexpected failures.
 ![image](https://github.com/ST10262825/PROG6221-POE/assets/128587768/3defbe25-917b-4e78-89b8-427c956ed561)

![image](https://github.com/ST10262825/PROG6221-POE/assets/128587768/3d41d03e-c70e-4fde-9b07-516a231edd40)

 
Calories Warning:
RecipeApp includes a warning system to alert users when a recipe has a high calorie count. If the total calories of a recipe exceed 300 calories, a warning message is displayed to notify the user. This helps users make informed decisions about their recipe choices and encourages healthier cooking practices.
![image](https://github.com/ST10262825/PROG6221-POE/assets/128587768/7b05fe05-b779-463a-9ee2-62e5a1af4aa6)

 
Lecturer Feedback
Based on the feedback provided by My lecturer, I made several enhancements to improve the RecipeApp:
- Implemented Clear Feedback and Error Handling:
I Added clear feedback messages after user prompts and confirmation messages to enhance user experience.
I Implemented robust error handling mechanisms to display informative error messages for invalid inputs and prevent unexpected behavior.
I Validated user inputs to ensure they meet specific criteria, such as non-empty fields and non-negative quantities.


Github Commits Screenshots
 
 

