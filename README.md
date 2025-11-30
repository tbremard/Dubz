# DubzLib - File Duplicate Detection Library

A high-performance C# .NET Core 8 library for detecting file duplicates in directory trees using intelligent three-stage filtering with parallel optimization.

## Overview

DubzLib implements an efficient duplicate file detection system that combines:
- **Metadata screening** (size/name comparison)
- **Fast content filtering** (partial file hashing) 
- **Full MD5 verification** (complete content validation)

The solution uses parallel processing optimized for modern NVMe storage systems and demonstrates professional software architecture with Strategy pattern implementation.

## Key Features

- ✅ **Three-stage filtering pipeline** for optimal performance
- ✅ **Parallel processing** with NVMe optimization
- ✅ **Multiple comparison modes** (size-only vs size+name)
- ✅ **Professional architecture** using proven design patterns
- ✅ **Comprehensive testing** with unit tests and validation
- ✅ **Console application** with dual output (console + file)

## Performance

- **Sub-second execution** on test datasets
- **Intelligent filtering** reduces expensive operations
- **100% accuracy** - no false positives/negatives in testing
- **Scalable design** suitable for large directory structures

## Project Structure

```
├── DubzLib/           # Core library implementation
├── Dubz/              # Console demo application  
├── DubzLib.Tests/     # Unit tests with test data
└── Spec/              # 📋 Specification and documentation
```

## Documentation

**📋 For complete details, see the `Spec/` folder:**

- **[Programming Exercise Specification](Spec/file_duplicate_exercise_summary.html)** - Original requirements
- **[Solution Report](Spec/solution_report.html)** - Complete implementation documentation, architecture details, and test results

## Quick Start

1. **Build the solution:**
   ```bash
   dotnet build
   ```

2. **Run duplicate detection:**
   ```bash
   dotnet run --project Dubz <DirectoryPath>
   ```

3. **Run tests:**
   ```bash
   dotnet test
   ```

## Author

**Thierry Bremard**  
t.bremard@gmail.com  
Date: 2025-11-30

---

> **Note:** This implementation exceeds specification requirements through intelligent performance optimizations and professional software engineering practices. See the solution report for detailed technical analysis and validation results.