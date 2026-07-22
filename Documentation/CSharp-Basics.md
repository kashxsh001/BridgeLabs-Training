#  C# Basics Documentation

# 📚 Topics Covered

- Introduction to C#
- Why C#?
- What is .NET?
- C# Architecture
- .NET Architecture
- CLR, CTS, CLS and CLI
- Roslyn Compiler
- IL (Intermediate Language)
- JIT Compiler
- Assembly (.dll/.exe)
- Namespace
- Entry Point (Main Method)
- Top-Level Statements
- Variables
- Data Types
- Type Conversion
- Input & Output
- Operators
- Operator Precedence
- Bitwise Operators

# 1. Introduction to C#

C# (C Sharp) is a modern, object-oriented programming language developed by Microsoft. It is primarily used to build:

- Console Applications
- Desktop Applications
- Web Applications
- REST APIs
- Cloud Applications
- Mobile Applications (MAUI/Xamarin)
- Games using Unity

# 2. Why C#?

- Object-Oriented
- Type Safe
- Automatic Memory Management (Garbage Collector)
- Rich Standard Library
- Cross Platform (.NET)
- High Performance
- Strong IDE Support (Visual Studio, VS Code)

# 3. What is .NET?

.NET is Microsoft's software development platform.

It provides:

- CLR (Runtime)
- Base Class Library (BCL)
- Compilers
- SDK
- Tools
- Runtime Environment

C# is a programming language.

.NET is the platform on which C# applications run.

# 4. C# Compilation Process

C# Source Code (.cs)
          │
          ▼
Roslyn Compiler
          │
          ▼
Intermediate Language (IL)
          │
          ▼
Assembly (.dll/.exe)
          │
          ▼
CLR
          │
          ▼
JIT Compiler
          │
          ▼
Machine Code
          │
          ▼
CPU Execution

# 5. Roslyn Compiler

Roslyn is the official C# compiler.

Responsibilities:

- Checks syntax
- Checks semantic errors
- Generates IL
- Generates Metadata
- Creates Assembly

# 6. Intermediate Language (IL)

IL is CPU-independent code generated after compilation.

Advantages:

- Platform Independent
- Language Independent
- Optimized before execution

# 7. CLR (Common Language Runtime)

CLR is the execution engine of .NET.

Responsibilities:

- Executes IL
- Memory Management
- Garbage Collection
- Exception Handling
- Security
- Thread Management
- JIT Compilation

# 8. JIT Compiler

JIT (Just-In-Time Compiler) converts IL into machine code at runtime.

IL
 │
 ▼
JIT
 │
 ▼
Machine Code

# 9. CTS (Common Type System)

CTS defines how data types are represented inside .NET.

Example:

All .NET languages understand:

- int
- string
- bool
- object

# 10. CLS (Common Language Specification)

CLS is a set of rules that every .NET language should follow to ensure interoperability.

Example:

Avoid language-specific features if you want libraries to work across multiple .NET languages.

# 11. CLI (Common Language Infrastructure)

CLI is the standard that defines:

- IL
- Metadata
- CLR
- CTS
- CLS

.NET is Microsoft's implementation of the CLI specification.

# 12. Assembly

An Assembly is the compiled output of a .NET project.

It can be:

- .dll
- .exe

An assembly contains:

- IL Code
- Metadata
- Manifest
- Resources

# 13. Namespace

Namespace is a logical grouping of related classes.

Example:

namespace College.Models
{

}

Benefits:

- Organizes code
- Avoids naming conflicts
- Improves readability

# 14. Main Method

The Main() method is the entry point of a console application.

Example:

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello");
    }
}

Unlike Java, Main() does **not** have to be public because the compiler stores the application's entry point in the assembly metadata, and the CLR invokes it directly.

# 15. Top-Level Statements

Modern C# allows writing code without explicitly creating a Main() method.

Example:

Console.WriteLine("Hello World");

The compiler automatically generates the Program class and Main() method.

# 16. Variables

A variable stores data.

Example:
int age = 20;
string name = "Kashish";

# 17. Data Types

## Value Types

- int
- long
- float
- double
- decimal
- char
- bool

## Reference Types

- string
- object
- class
- interface
- array

# 18. Input & Output

Output:

Console.WriteLine("Hello");

Input:

string name = Console.ReadLine();


Reading Integer:

int age = int.Parse(Console.ReadLine());

Safe Conversion:

int.TryParse(Console.ReadLine(), out int age);

# Parse vs TryParse

| Parse | TryParse |
|---------|----------|
| Throws Exception | Returns true/false |
| Unsafe | Safe |
| Use when input is guaranteed valid | Use for user input |

# 19. Pass by Value vs Pass by Reference

Value Types:

A copy of the value is passed.

Reference Types:

The reference is copied.

Both variables point to the same object.

Using **ref** passes the reference itself by reference.

# 20. Operators

## Arithmetic

- +
- -
- *
- /
- %

---

## Relational

- >
- <
- >=
- <=
- ==
- !=

---

## Logical

- &&
- ||
- !

---

## Assignment

- =
- +=
- -=
- *=
- /=

---

## Unary

- ++
- --
- !
- +

---

## Bitwise

- &
- |
- ^
- ~
- <<
- >>

---

# 21. Operator Precedence

Highest → Lowest

```text
()
↓
Unary
↓
* / %
↓
+ -
↓
<< >>
↓
< > <= >=
↓
== !=
↓
&
↓
^
↓
|
↓
&&
↓
||
↓
??
↓
?:
↓
Assignment


# 22. Bitwise Operators

Binary uses powers of 2.

8   4   2   1

Example:

5 = 0101

3 = 0011

AND

0101
0011
----
0001

Result = 1

OR

0101
0011
----
0111

Result = 7

XOR

0101
0011
----
0110

Result = 6

# Key Takeaways

- C# runs on the .NET platform.
- Source code is compiled into IL.
- CLR executes IL using the JIT compiler.
- Assemblies are compiled output files (.dll/.exe).
- Namespaces organize code.
- Main() is the application's entry point.
- TryParse is safer than Parse.
- Reference types pass a copy of the reference.
- Bitwise operators work on binary representations.