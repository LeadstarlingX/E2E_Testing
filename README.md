# SauceDemo E2E Testing Framework

A robust, page-object-model based automated testing framework for [SauceDemo](https://www.saucedemo.com/), built with C# (NUnit + Selenium).

## Features
- **Page Object Model (POM)**: Organized and reusable components (`LoginPage`, `InventoryPage`, `CartPage`, `CheckoutPage`).
- **Strict Git Workflow**: Development > Master branching strategy.
- **CI/CD**: GitHub Actions workflow for automated testing on push.
- **Cross-Browser Ready**: Built on Selenium WebDriver (Chrome).

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Google Chrome

### Installation
1. Clone the repository:
   ```bash
   git clone git@github.com:LeadstarlingX/E2E_Testing.git
   ```
2. Navigate to the project directory:
   ```bash
   cd EndToEndTesting
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```

## Running Tests

### Run all tests
```bash
dotnet test
```

### Run specific tests
```bash
dotnet test --filter "FullyQualifiedName~LoginTests"
dotnet test --filter "FullyQualifiedName~InventoryTests"
```

## Project Structure
- `Pages/`: Contains the Page Objects.
- `Tests/`: Contains the NUnit test classes.
- `.github/workflows/`: CI/CD configuration.

## Contributing
1. Create a feature branch (`feat/your-feature`).
2. Commit your changes (keep messages under 50 chars).
3. Merge to `development`.
