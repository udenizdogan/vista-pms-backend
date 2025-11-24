# VistaPms Backend

.NET 9 ile geliştirilmiş Clean Architecture prensiplerine uygun bir Property Management System (PMS) backend projesi.

## 🏗️ Proje Yapısı

```
VistaPms/
├── VistaPms.Domain/          # Domain katmanı (Entity, ValueObject, Enum, Events)
├── VistaPms.Application/     # Application katmanı (CQRS, DTOs, Interfaces)
├── VistaPms.Infrastructure/  # Infrastructure katmanı (EF Core, Identity, Repositories)
└── VistaPms.API/            # API katmanı (Controllers, Middlewares, SignalR)
```

## 🚀 Teknolojiler

- **.NET 9.0**
- **Entity Framework Core 9.0** (PostgreSQL)
- **MediatR** (CQRS pattern)
- **FluentValidation** (Validation)
- **Mapster** (Object mapping)
- **ASP.NET Core Identity** (Authentication & Authorization)
- **JWT Bearer** (Token-based authentication)
- **SignalR** (Real-time communication)
- **Swagger/OpenAPI** (API documentation)

## 📋 Özellikler

- ✅ Clean Architecture
- ✅ CQRS Pattern (MediatR)
- ✅ Repository Pattern
- ✅ Multi-Tenancy Support
- ✅ JWT Authentication
- ✅ Role-Based Authorization
- ✅ Global Exception Handling
- ✅ FluentValidation
- ✅ SignalR Real-time Hub
- ✅ Swagger Documentation

## 🔧 Kurulum

### Gereksinimler

- .NET 9 SDK
- PostgreSQL 14+
- Visual Studio 2022 / VS Code / Rider

### Adımlar

1. **Repository'yi klonlayın:**
```bash
git clone <repository-url>
cd vista-pms-backend
```

2. **Bağlantı dizesini yapılandırın:**

`VistaPms.API/appsettings.json` dosyasındaki connection string'i güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=VistaPmsDb;Username=postgres;Password=your_password"
  }
}
```

3. **Migration'ları çalıştırın:**
```bash
cd VistaPms.API
dotnet ef migrations add InitialCreate --project ../VistaPms.Infrastructure
dotnet ef database update
```

4. **Uygulamayı çalıştırın:**
```bash
dotnet run --project VistaPms.API
```

5. **Swagger UI'a erişin:**
```
https://localhost:5001/swagger
```

## 📡 API Endpoints

### Authentication
- `POST /api/auth/register` - Yeni kullanıcı kaydı
- `POST /api/auth/login` - Kullanıcı girişi

### Rooms
- `GET /api/rooms` - Tüm odaları listele
- `POST /api/rooms` - Yeni oda oluştur

### SignalR Hub
- `/realtime` - Real-time notification hub

## 🗄️ Database Schema

### Entities
- **Room** - Otel odaları
- **Guest** - Misafirler
- **Reservation** - Rezervasyonlar

Tüm entity'ler `BaseEntity`'den türer ve şunları içerir:
- `Id` (Guid)
- `TenantId` (Multi-tenancy)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime?)

## 🔐 Authentication & Authorization

Proje JWT Bearer token authentication kullanır. Her istek için `Authorization` header'ında token gönderilmelidir:

```
Authorization: Bearer <your-jwt-token>
```

### Roller
- **User** - Standart kullanıcı
- **Admin** - Yönetici (ileride eklenecek)

## 🏛️ Clean Architecture Katmanları

### Domain Layer
- Hiçbir external dependency yok
- Entity'ler, Value Object'ler, Enum'lar
- Domain event'ler

### Application Layer
- Domain'e bağımlı
- MediatR handlers (Commands & Queries)
- DTOs, Validators
- Interface'ler

### Infrastructure Layer
- Application ve Domain'e bağımlı
- EF Core DbContext
- Repository implementasyonları
- Identity & Token servisleri

### API Layer
- Tüm katmanlara bağımlı
- Controllers
- Middlewares
- SignalR Hubs
- Dependency Injection configuration

## 📝 Örnek Kullanım

### 1. Kullanıcı Kaydı
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "Password123!",
    "firstName": "John",
    "lastName": "Doe",
    "tenantId": "tenant-001"
  }'
```

### 2. Giriş Yapma
```bash
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "Password123!"
  }'
```

### 3. Oda Oluşturma
```bash
curl -X POST https://localhost:5001/api/rooms \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <your-token>" \
  -d '{
    "roomNumber": "101",
    "roomType": "Deluxe",
    "pricePerNight": 150.00,
    "floor": 1,
    "maxOccupancy": 2,
    "description": "Deniz manzaralı deluxe oda"
  }'
```

## 🧪 Testing

```bash
# Unit testler (ileride eklenecek)
dotnet test
```

## 📦 NuGet Packages

### Domain
- Hiçbir paket yok (Pure domain logic)

### Application
- MediatR
- FluentValidation
- Mapster
- Microsoft.EntityFrameworkCore

### Infrastructure
- Npgsql.EntityFrameworkCore.PostgreSQL
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- System.IdentityModel.Tokens.Jwt

### API
- Swashbuckle.AspNetCore
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.EntityFrameworkCore.Design

## 🤝 Katkıda Bulunma

1. Fork edin
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Commit edin (`git commit -m 'Add some amazing feature'`)
4. Push edin (`git push origin feature/amazing-feature`)
5. Pull Request açın

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## 👥 İletişim

Proje Sahibi - [@umutdenizdogan](https://github.com/umutdenizdogan)

Proje Linki: [https://github.com/umutdenizdogan/vista-pms-backend](https://github.com/umutdenizdogan/vista-pms-backend)
