# VistaPms Domain Model - Comprehensive Documentation

## 📊 Overview

VistaPms.Domain projesi için DDD (Domain-Driven Design) prensiplerine uygun, tamamen persistence-agnostic bir domain model oluşturuldu.

**Toplam Dosya Sayısı:** 35 adet C# dosyası

## ✅ Genel Kurallar (Uygulandı)

- ✅ Hiçbir ORM attribute veya EF Core bağımlılığı yok
- ✅ Tüm entity'ler BaseEntity'den türüyor (Id, TenantId, CreatedAt, UpdatedAt)
- ✅ ValueObject yapıları immutable (init-only properties)
- ✅ Navigation property'ler yalnızca koleksiyon veya entity referansı
- ✅ Koleksiyonlar daima `IReadOnlyCollection<T>` + private backing field

---

## 📦 Entities (17 adet)

### Core Entities

#### 1. **BaseEntity** (Abstract)
Tüm entity'lerin base class'ı
- `Id` (Guid)
- `TenantId` (string) - Multi-tenancy support
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime?)
- Domain Events support

#### 2. **Building**
Otel binası
- `Name`
- `Description`
- Navigation: `Floors` (IReadOnlyCollection)

#### 3. **Floor**
Kat bilgisi
- `Name`
- `Order` (int)
- `BuildingId`
- Navigation: `Building`, `Rooms`

#### 4. **RoomType**
Oda tipi (Deluxe, Suite, Standard, vb.)
- `Name`
- `Description`
- `BasePrice` (decimal)
- `DefaultCapacity` (int)
- `Amenities` (IReadOnlyCollection<RoomAmenity>)
- Navigation: `Rooms`, `RatePlans`

#### 5. **Room** ⭐ Aggregate Root
Oda entity'si
- `Number`
- `FloorId`
- `RoomTypeId`
- `Capacity` (int)
- `Status` (RoomStatus enum)
- `HousekeepingStatus` (HousekeepingStatus enum)
- `IsActive` (bool)
- `ActiveRatePlanId` (Guid?)
- `Notes` (string?)
- Navigation: `Floor`, `RoomType`, `ActiveRatePlan`, `Reservations`, `MinibarItems`, `MaintenanceTickets`, `HousekeepingTasks`

### Guest & Reservation

#### 6. **Guest** ⭐ Aggregate Root
Misafir bilgileri
- `FirstName`
- `LastName`
- `Phone`
- `Email`
- `Address`
- `Nationality`
- Computed: `FullName` property
- Navigation: `Reservations`, `Folios`

#### 7. **Reservation** ⭐ Aggregate Root
Rezervasyon
- `RoomId`
- `GuestId`
- `CheckIn` (DateTime)
- `CheckOut` (DateTime)
- `Adults` (int)
- `Children` (int)
- `Status` (ReservationStatus enum)
- `RatePlanId`
- `TotalPrice` (decimal)
- `FolioId` (Guid?)
- Computed: `TotalNights` property
- Navigation: `Room`, `Guest`, `RatePlan`, `Folio`

#### 8. **RatePlan** ⭐ Aggregate Root
Fiyat planı / Sezon
- `Name`
- `Description`
- `StartDate`
- `EndDate`
- `PricePerNight` (decimal)
- `RoomTypeId`
- `MinStay` (int?)
- `MaxStay` (int?)
- `CancellationPolicies` (IReadOnlyCollection)
- `DerivedRates` (IReadOnlyCollection<RatePlan>)
- Method: `IsActiveOn(DateTime date)`
- Navigation: `RoomType`, `Reservations`

### Folio & Billing

#### 9. **Folio** ⭐ Aggregate Root
Hesap/Fatura
- `FolioNumber`
- `ReservationId` (Guid?)
- `GuestId`
- `Status` (FolioStatus enum)
- Computed: `TotalCharges`, `TotalPayments`, `Balance`
- Navigation: `Reservation`, `Guest`, `Charges`, `Payments`, `POSOrders`

#### 10. **FolioCharge**
Hesaba eklenen ücret
- `FolioId`
- `Description`
- `Amount` (decimal)
- `ProductId` (Guid?)
- `ChargeType` (ChargeType enum)
- Navigation: `Folio`, `Product`

#### 11. **FolioPayment**
Ödeme kaydı
- `FolioId`
- `Amount` (decimal)
- `Method` (PaymentMethod enum)
- `ReferenceNumber`
- Navigation: `Folio`

### Product & POS

#### 12. **ProductCategory** ⭐ Aggregate Root
Ürün kategorisi
- `Name`
- `Description`
- Navigation: `Products`

#### 13. **Product** ⭐ Aggregate Root
Ürün (POS & Minibar)
- `Name`
- `CategoryId`
- `Price` (decimal)
- `Barcode`
- `IsActive` (bool)
- Navigation: `Category`, `OrderItems`, `FolioCharges`

#### 14. **POSOrder** ⭐ Aggregate Root
POS siparişi
- `FolioId` (Guid?)
- `TotalAmount` (decimal)
- `Status` (POSOrderStatus enum)
- Method: `CalculateTotal()`
- Navigation: `Folio`, `Items`

#### 15. **POSOrderItem**
Sipariş kalemi
- `POSOrderId`
- `ProductId`
- `Quantity` (int)
- `Price` (decimal)
- `Total` (decimal)
- Method: `CalculateTotal()`
- Navigation: `POSOrder`, `Product`

### Maintenance & Housekeeping

#### 16. **MaintenanceTicket** ⭐ Aggregate Root
Bakım/Arıza talebi
- `RoomId` (Guid?)
- `CreatedByUserId`
- `AssignedToUserId`
- `Description`
- `Status` (MaintenanceStatus enum)
- `Priority` (MaintenancePriority enum)
- `Photos` (IReadOnlyCollection<MaintenancePhoto>)
- Navigation: `Room`

#### 17. **HousekeepingTask** ⭐ Aggregate Root
Temizlik görevi
- `RoomId`
- `AssignedUserId`
- `TaskType` (HousekeepingTaskType enum)
- `DueDate` (DateTime)
- `Status` (MaintenanceStatus enum)
- `Notes`
- Navigation: `Room`

---

## 💎 Value Objects (7 adet)

### 1. **RoomAmenity**
Oda olanakları
- `Name`
- `Icon`

### 2. **CancellationPolicy**
İptal politikası
- `DaysBeforeCheckIn` (int)
- `PenaltyPercentage` (decimal)
- `Description`

### 3. **MaintenancePhoto**
Bakım fotoğrafı
- `Url`
- `CreatedAt` (DateTime)

### 4. **Money**
Para birimi
- `Amount` (decimal)
- `Currency` (string, default: "USD")
- Operators: `+`, `-` (currency validation)

### 5. **DateRange**
Tarih aralığı
- `Start` (DateTime)
- `End` (DateTime)
- Computed: `DurationInDays`
- Methods: `Overlaps(DateRange)`, `Contains(DateTime)`

### 6. **Address**
Adres
- `Street`
- `City`
- `Country`
- `PostalCode` (optional)
- Computed: `FullAddress`

### 7. **PhoneNumber**
Telefon numarası
- `CountryCode`
- `Number`
- Computed: `FullNumber`
- Override: `ToString()`

---

## 🎯 Enums (10 adet)

### 1. **RoomStatus**
- Vacant
- Occupied
- Dirty
- OutOfService

### 2. **HousekeepingStatus**
- Clean
- Dirty
- Inspected

### 3. **ReservationStatus**
- Pending
- Confirmed
- CheckedIn
- CheckedOut
- Cancelled

### 4. **FolioStatus**
- Open
- Closed

### 5. **ChargeType**
- RoomCharge
- POS
- Minibar
- ExtraService

### 6. **PaymentMethod**
- Cash
- CreditCard
- AgencyCredit

### 7. **MaintenanceStatus**
- Open
- InProgress
- Completed

### 8. **MaintenancePriority**
- Low
- Medium
- High

### 9. **HousekeepingTaskType**
- Clean
- Inspect
- LinenChange
- CommonArea

### 10. **POSOrderStatus**
- Pending
- Completed
- Cancelled

---

## 🔗 Entity Relationships

### Room Relationships
```
Room
├── Floor (Many-to-One)
├── RoomType (Many-to-One)
├── ActiveRatePlan (Many-to-One, optional)
├── Reservations (One-to-Many)
├── MinibarItems (Many-to-Many via Product)
├── MaintenanceTickets (One-to-Many)
└── HousekeepingTasks (One-to-Many)
```

### Reservation Flow
```
Guest → Reservation → Room
              ↓
           RatePlan
              ↓
            Folio
              ↓
      Charges + Payments
```

### POS Flow
```
Product → POSOrderItem → POSOrder → Folio
   ↓
Category
```

### Maintenance Flow
```
Room → MaintenanceTicket
         ↓
   MaintenancePhoto (Value Object)
```

---

## 📈 Aggregate Roots

Toplam **12 Aggregate Root** entity:
1. Building
2. Floor
3. RoomType
4. Room
5. Guest
6. Reservation
7. RatePlan
8. Folio
9. ProductCategory
10. Product
11. POSOrder
12. MaintenanceTicket
13. HousekeepingTask

---

## ✨ Domain Model Özellikleri

### Immutability
- Tüm Value Object'ler immutable (init-only properties)
- Protected parameterless constructor'lar EF Core için

### Encapsulation
- Private backing fields (`_items`)
- Public `IReadOnlyCollection<T>` properties
- Koleksiyonlar dışarıdan değiştirilemez

### Business Logic
- `Folio.Balance` (calculated property)
- `Reservation.TotalNights` (calculated property)
- `DateRange.Overlaps()` (domain logic)
- `Money` operator overloading (+, -)
- `POSOrder.CalculateTotal()` (method)

### Validation
- `DateRange` constructor validation
- `Money` operator currency validation

---

## 🏗️ Klasör Yapısı

```
VistaPms.Domain/
├── Entities/
│   ├── BaseEntity.cs
│   ├── Building.cs
│   ├── Floor.cs
│   ├── RoomType.cs
│   ├── Room.cs
│   ├── Guest.cs
│   ├── Reservation.cs
│   ├── RatePlan.cs
│   ├── Folio.cs
│   ├── FolioCharge.cs
│   ├── FolioPayment.cs
│   ├── ProductCategory.cs
│   ├── Product.cs
│   ├── POSOrder.cs
│   ├── POSOrderItem.cs
│   ├── MaintenanceTicket.cs
│   └── HousekeepingTask.cs
├── ValueObjects/
│   ├── RoomAmenity.cs
│   ├── CancellationPolicy.cs
│   ├── MaintenancePhoto.cs
│   ├── Money.cs
│   ├── DateRange.cs
│   ├── Address.cs
│   └── PhoneNumber.cs
├── Enums/
│   ├── RoomStatus.cs
│   ├── HousekeepingStatus.cs
│   ├── ReservationStatus.cs
│   ├── FolioStatus.cs
│   ├── ChargeType.cs
│   ├── PaymentMethod.cs
│   ├── MaintenanceStatus.cs
│   ├── MaintenancePriority.cs
│   ├── HousekeepingTaskType.cs
│   └── POSOrderStatus.cs
├── Events/
│   └── (boş - ileride domain event'ler eklenecek)
└── Interfaces/
    └── IAggregateRoot.cs
```

---

## ✅ Build Status

```bash
dotnet build VistaPms.Domain/VistaPms.Domain.csproj
```

**Result:** ✅ Build succeeded

---

## 🎓 DDD Prensipleri

### Uygulandı ✅
- Aggregate Root pattern
- Value Object immutability
- Rich domain model (business logic in entities)
- Ubiquitous Language (domain terminology)
- Bounded Context (hotel management)
- No infrastructure dependencies

### Öneriler
- Domain Event'ler eklenebilir (örn: `RoomReserved`, `CheckInCompleted`)
- Specification pattern bazı sorgular için kullanılabilir
- Domain Service'ler karmaşık business logic için eklenebilir

---

## 📝 Sonuç

VistaPms.Domain projesi için **kapsamlı, temiz ve bağımsız** bir domain model oluşturuldu:

- **17 Entity** (1 BaseEntity + 16 domain entity)
- **7 Value Object**
- **10 Enum**
- **1 Interface** (IAggregateRoot)

Toplam **35 dosya**, tamamı DDD prensiplerine uygun, persistence-agnostic ve ORM-free.
