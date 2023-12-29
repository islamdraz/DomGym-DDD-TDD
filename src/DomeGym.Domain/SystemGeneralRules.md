**First rules**

## this part of ubiquitous language

1. A **user** can **create** a **participant profile**
2. Participants can reserve a spot in a session
3. A session takes place in a room
4. A session has single trainer and a maximum number of participants
5. A **gym** can have **multiple** **rooms**
6. A **user** can **create** an **admin profile**

7. an **admin** can have an **active subscription**
8. a subscription can have multiple gyms
9. an active subscription can be of type free, starter, or pro.
10. A user can create a **trainer profile**
11. A trainer can **teach sessions** across gyms and subscription

-------------------------------

### User

### Participant

### Admin

### Trainer

### Subscription

### Gym

### Room

### Session

------------------------

## Invariants

## session invariants

1. A session cannot contain more than maximum number of participants
2. A reservation cannot be canceled for free less than 24 hours before the session starts

## Gym invarians

1. A gym can't have rooms more than the subscription allows

## Room Invariance

1. A room cannot have more session than the subscription allows
2. A room cannot have two or more overlapping sessions

## Subscription invariants

1. A subscription cannot have more gyms than the subscription allows

## Trainer Invariants

1. A trainer cannot teach two or more overlapping sessions

## Participant Invariants

1. A participant cannot reserve overlapping sessions
