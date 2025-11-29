# DubzLib - File Duplicate Detection Library

## Project Context

This is a C# .NET Core 8 library for detecting file duplicates in directory trees. The project is part of a programming exercise for firm selection process at Schleupen.

## Specification

The complete specification is available in: `Spec/file_duplicate_exercise_summary.html`

### Key Requirements
- **Language**: C# .NET Core 8
- **Two-step duplicate detection**:
  1. `SammleKandidaten()` - Fast metadata screening (size/name)
  2. `PruefeKandidaten()` - Content verification using MD5 hash
- **Comparison modes**: Size only (`Groesse`) vs Size+Name (`GroesseUndName`)
- **Return pattern**: Use "return ret" pattern for all return variables

## Project Structure

```
Dubz.sln
├── DubzLib/                    # Main library
│   ├── IDublette.cs           # Interface for duplicate file groups
│   ├── IDublettenpruefung.cs  # Main API interface
│   ├── Vergleichsmodi.cs      # Comparison mode enum
│   ├── Dubletten.cs           # Main implementation (partial)
│   ├── Item.cs                # Helper class for file metadata
│   └── DubletteImpl.cs        # IDublette implementation
├── Dubz/                      # Console demo application
│   └── Program.cs
└── DubzLib.Tests/             # Unit tests
    ├── DublettenTest.cs       # Test cases
    └── TargetFakeDir/         # Test file structure (see below)
```

## Current Implementation Status

### ✅ Completed
- All interfaces defined per specification
- `BuildItems()` method - recursive file discovery with grouping keys
- `Item` helper class for file metadata
- Basic project structure and dependencies

### 🚧 In Progress  
- `SammleKandidaten()` - needs grouping logic to convert Items to IDublette
- Currently returns `null` (line 25 in Dubletten.cs)

### ❌ Not Started
- `PruefeKandidaten()` - MD5 content verification
- Console application completion
- Full unit test suite

## Test Strategy

### Test File Structure (`DubzLib.Tests/TargetFakeDir/`)

**Directory Layout:**
```
TargetFakeDir/                 (4 files)
├── A/                        (6 files)  
│   └── A/                    (2 files)
└── B/                        (7 files)
Total: 19 test files
```

**Test Scenarios:**
- **Exact duplicates**: `duplicate.txt` (3 copies), `small.txt` (2 copies)
- **Same name, different content**: `file1.txt` (4 locations)
- **Same size, different names**: `samesize.txt` variants
- **Same content, different names**: `identical_content.txt`/`different_name.txt`
- **Unique files**: No duplicates

**Build Integration:**
- Test files are automatically copied to build output via csproj Content item
- Allows tests to run against known file structure

### Testing Approach
1. `BuildItems()` test verifies recursive file discovery
2. Future tests will validate grouping logic and MD5 verification
3. Both comparison modes (`Groesse` vs `GroesseUndName`) will be tested

## Development Notes

### German Naming Convention
- `pfad` = path (directory path)
- `Dubletten` = duplicates
- `Vergleichsmodi` = comparison modes
- `SammleKandidaten` = collect candidates  
- `PruefeKandidaten` = verify candidates

### Key Implementation Details
- `BuildItems()` uses manual recursion instead of `Directory.GetFiles(..., AllDirectories)`
- `CreateKey()` generates grouping keys: `{mode}::{size}` or `{mode}::{size}::{name}`
- Recent fix: `Item` constructor now uses `filePath` directly (was incorrectly using `curPath`)

## Commands

To run tests:
```bash
dotnet test
```

To run console app:
```bash
dotnet run --project Dubz <DirectoryPath>
```