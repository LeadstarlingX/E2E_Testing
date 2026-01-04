# SauceDemo E2E Testing Framework

A robust, scalable end-to-end testing suite for [SauceDemo](https://www.saucedemo.com/) built using **C#**, **NUnit**, and **Selenium WebDriver**.

## 🚀 Key Features

- **Page Object Model (POM)**: Clean separation of UI elements and test logic.
- **Robust Synchronization**: Intelligent use of Implicit and Explicit waits to eliminate flakiness.
- **Data-Driven Testing**: Full test coverage across multiple user profiles (`standard_user`, `problem_user`, `performance_glitch_user`, etc.).
- **Centralized Configuration**: All URLs, credentials, and timeouts managed in a single `Constants` class.
- **CI/CD Ready**: Integrated with GitHub Actions for automated builds and testing.

## 🛡️ Stability & reliability (The "Wait" Strategy)

One of the biggest challenges in E2E automation is timing. This framework implements a dual-wait strategy to ensure maximum reliability:

### Implicit Waits
We set a global **Implicit Wait** (5 seconds) in `BaseTest`. This acts as a background safety net, automatically polling the DOM for elements that might be slightly delayed.

### Explicit Waits
For specific interactions, we use **Explicit Waits** (`WebDriverWait`) within our Page Objects. This is critical for:
- **Performance Glitch User**: Handling the intentional 5-second delays in SauceDemo.
- **Dynamic Elements**: Waiting for side menus to finish animating or cart badges to update.
- **Custom Conditions**: Waiting until an element is not just present, but also *clickable* or *visible*.

## 🏗️ Architecture

### 1. Page Objects (`/Pages`)
Each page (Login, Inventory, Cart, etc.) has its own dedicated class. This makes the tests readable and shifts maintenance to a single location when the UI changes.

### 2. Base Classes
- **`BasePage`**: Contains the WebDriver instance and a configurable `Wait()` method for micro-delays between actions to mimic human behavior.
- **`BaseTest`**: Handles browser initialization (Chrome), anti-bot measures, and automatic session cleanup.

### 3. User Matrix Testing
The framework validates SauceDemo's business logic across the user spectrum:
- **Standard User**: Verifies the happy path and math calculations.
- **Problem User**: Detects broken UI elements and images.
- **Performance User**: Validates robustness against slow responses.
- **Locked Out User**: Confirms security error handling.

## 🛠️ Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Google Chrome](https://www.google.com/chrome/)

### Running Tests
1. Clone the repository.
2. Open in Visual Studio or VS Code.
3. Run from the terminal:
   ```bash
   dotnet test
   ```

## 📈 CI/CD
The project includes a `.github/workflows/build.yaml` file that automatically:
1. Restores dependencies.
2. Builds the solution.
3. Executes the full test suite on every push to the `development` branch.
