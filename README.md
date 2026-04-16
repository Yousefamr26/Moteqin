 Moteqin | Quran Memorization & Tracking System

 Overview
Moteqin is a backend system designed to help students memorize the Quran efficiently while enabling instructors (Sheikhs) to monitor progress, review recitations, and provide structured feedback.

The system addresses a real-world problem by digitizing the memorization workflow and making it more organized, trackable, and efficient.

---

 Objectives
- Help students track their memorization progress
- Provide a structured workflow for recitation and feedback
- Improve consistency through daily plans and gamification
- Enable instructors to monitor and guide students effectively

---

 Features

### Authentication & Authorization
- User Registration and Login
- JWT Authentication
- Role-based access (Student / Sheikh)

---

### Quran Module
- Retrieve Surahs and Ayahs
- Organized structure for memorization

---

### Progress Tracking
- Add and update memorization progress
- Track memorized Ayahs
- Monitor consistency

---

### Recordings System
- Upload recitations
- Manage recordings
- Enable instructor review

---

### Feedback System
- Instructor reviews recordings
- Add structured feedback
- Students can view feedback

---

### Daily Plan
- Generate daily memorization plans
- Track plan completion

---

### Groups System
- Join memorization groups
- Monitor group members
- Support collaborative learning

---

### Gamification
- Points system
- Streak tracking
- Leaderboard
- Rank system

---

### Reports
- Weekly progress reports
- Monthly progress reports
- Active days tracking

---

## Architecture

The project follows Clean Architecture principles:

- Presentation Layer (Controllers)
- Application Layer (CQRS + MediatR)
- Domain Layer (Entities and Business Logic)
- Infrastructure Layer (EF Core and Repositories)
- ## Project Structure

Moteqin  
│  
├── Domain  
├── Application  
├── Infrastructure  
└── Presentation  

---

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- Clean Architecture
- CQRS Pattern
- MediatR
- JWT Authentication
- Repository Pattern
- Unit of Work

---

## Design Approach

This project is built using the MVP (Minimum Viable Product) approach:

- Focus on delivering core features first
- Ensure full end-to-end functionality
- Maintain scalability and extensibility

---

## Future Enhancements

- Notifications system
- Smart memorization recommendations
- Performance optimization (caching)
- Mobile application integration
- Advanced analytics dashboard

---

## API Endpoints (Sample)

POST   /api/auth/register  
POST   /api/auth/login  

GET    /api/quran/surahs  
GET    /api/quran/ayahs/{surahId}  

POST   /api/progress  
GET    /api/progress  

POST   /api/recordings  
GET    /api/recordings  

POST   /api/feedback  
GET    /api/feedback  

GET    /api/reports/weekly  
GET    /api/reports/monthly  

---


---

## Key Learnings

- Building a complete backend system from scratch
- Applying Clean Architecture in a real-world project
- Implementing CQRS using MediatR
- Designing scalable and maintainable systems
- Managing real-world workflows between students and instructors





Feedback and suggestions are welcome.
