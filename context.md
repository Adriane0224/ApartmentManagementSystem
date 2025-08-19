## Contexts
- Directory — Tenants, Owners
- Property — Buildings, Units, UnitStatus
- Leasing — Lease lifecycle (create/renew/terminate)
- Billing — Rent Payments

## Relationships
- Leasing uses Tenant (Directory) and Unit (Property)
- Billing uses Lease (Leasing)
- Property updates UnitStatus on lease events (LeaseCreated, LeaseTerminated)

## Aggregates & Invariants
- Directory: Tenant, Owner
- Property: Building (owns Units)
- Leasing: Lease (only one open lease per Unit; End > Start)
- Billing: Payment (Amount > 0)
