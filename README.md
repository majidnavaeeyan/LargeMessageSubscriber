# Large Message Subscriber



Large Message Subscriber
Objective
Design and implement a system to process, store, and retrieve time-series data,
showcasing your proficiency in backend development using ASP.NET Core and modern
software development principles.

## Data Flow
1. Data Ingestion:

  * Receive data messages from a Message Broker (e.g., RabbitMQ or an
equivalent).

![03](https://github.com/user-attachments/assets/2c83fbcb-23b2-48f3-a107-c2ed03580893)


  * Each message contains over 5,000 data points, each with the following structure:

            Name: A unique string identifier for the data point.
            Timestamp: The exact time of the data point.
            Value: A floating-point number.


![10](https://github.com/user-attachments/assets/4210517f-7b75-4dc7-86b8-b2310b75f686)



  * Process incoming messages at a rate of one message per second.


  * Candidates are required to generate and publish sample data to simulate
the subscription process and ensure the system processes it correctly.

2. Data Storage:

  * Store data in InfluxDB using three distinct buckets:
1. Daily Bucket:

  * Stores raw data for the last 30 days.

2. Monthly Bucket:

  * Stores hourly aggregated data (average, minimum, maximum) for the last 12 months.

3. Yearly Bucket:

![04](https://github.com/user-attachments/assets/207dead8-257d-4f4d-bc04-b64223a109c2)


  * Stores daily aggregated data (average, minimum, maximum)
indefinitely.

  * Automatically transform raw data into aggregated formats for monthly and
yearly buckets.

3. Data Distribution:

* Publish processed data back to a Message Broker.


* Provide a WebSocket endpoint for clients to receive real-time updates.

4. Data Retrieval API:

* Implement an API to query stored data based on the following parameters:

  * Data Point Name.


  * Time Precision (raw, hourly, or daily).


  * Start Date/Time and End Date/Time.


* Determine the appropriate bucket to query based on input parameters.


* The API should use minimal JWT authentication to secure access.

5. Docker Deployment:

* The project should be containerized.


* Provide a Docker-Compose file to set up and run the system, including
dependencies like the Message Broker and InfluxDB.

## Requirements and Considerations
1. Architecture

* Use Onion Architecture or a layered architecture approach.

![06](https://github.com/user-attachments/assets/61928ef6-7aca-46cf-b9aa-25aa60b75334)


* Ensure separation of concerns by isolating business logic, data access, and
presentation layers.


* The architecture should be flexible enough to allow replacement of components
(e.g., Message Broker or database) with minimal changes.

2. Error Handling

* Implement a robust error-handling strategy:

* Log all errors with sufficient details.


* Provide clear and actionable error messages to clients via the API.


* Ensure data is not lost during system failures.

3. Logging

* Include diagnostic logging for:

  * Key processing milestones (e.g., message ingestion, data aggregation).


  * Errors and warnings.


* Contextual information such as request IDs and user details.

  ![08](https://github.com/user-attachments/assets/d7b63f0e-a2da-48f8-86a8-dd9c3c78fe75)


4. Validation

* Validate all API inputs to ensure correctness and avoid unexpected failures.

5. Testing


* Include comprehensive tests:

  * Unit Tests for individual components and business logic.


  * Integration Tests to verify interactions between the system and external
dependencies (e.g., database, Message Broker).

  * Ensure tests cover edge cases and critical paths.

6. Data Retention and Management

* Implement data retention policies:

  * Automatically delete data older than 30 days from the daily bucket.


  * Optimize storage by ensuring aggregated data is accurate and consistent.
 
 ```
option task = {name: "MonthlyAggregate", every: 30d}

from(bucket: "Daily_Bucket")
    |> range(start: -30d)
    |> filter(fn: (r) => r._measurement == "myMeasurment")
    |> group(columns: ["_field"])
    |> reduce(
        identity: {min: 1.0, max: -1.0, sum: 0.0, count: 0},

        fn:
            (accumulator, r) =>
                ({
                    min: if r._value < accumulator.min then r._value else accumulator.min,
                    max: if r._value > accumulator.max then r._value else accumulator.max,
                    sum: accumulator.sum + float(v: r._value),
                    count: accumulator.count + 1,
                }),
    )
    |> map(
        fn: (r) =>
            ({
                _time: now(),
                field_name: r._field,
                min_value: r.min,
                max_value: r.max,
                mean_value: r.sum / float(v: r.count),

            }),
    )
    |> to(bucket: "Monthly_Bucket", org: "d9a201a05434532d")


 ```
 
  ![01](https://github.com/user-attachments/assets/f10ad31a-7aa1-4366-9041-876c1e801da5)

  ```
option task = { 
  name: "YearlyAggregate",
  every: 12mo,
}

from(bucket: "Monthly_Bucket")
  |> range(start: -12mo)
  |> filter(fn: (r) => r._measurement == "myMeasurment")
  |> group(columns: ["_field"])
  |> reduce(

      identity: {min: 1.0, max: -1.0, sum: 0.0, count: 0},
      fn: (accumulator, r) => ({
          min: if r._value < accumulator.min then r._value else accumulator.min,
          max: if r._value > accumulator.max then r._value else accumulator.max,
          sum: accumulator.sum + float(v: r._value),
          count: accumulator.count + 1
      })
  )
  |> map(fn: (r) => ({
        _time: now(),
        field_name: r._field,
        min_value: r.min,
        max_value: r.max,
        mean_value: r.sum / float(v: r.count)
  }))
  |> to(bucket: "Yearly_Bucket", org: "d9a201a05434532d")

```

  ![02](https://github.com/user-attachments/assets/d3f04b20-f6c4-4b75-b218-69cee7e87307)


7. Code Quality

* Follow clean code principles and ensure the code is:

  * Readable and maintainable.


  * Free from code smells and unnecessary complexity.

8. Git and Documentation

* Use a Git Repository for version control.


* Commits should have meaningful messages (e.g., Implement data
aggregation for monthly bucket).

* Provide a README.md file with:

  * Project overview and objectives.


  * Key features and implementation details.


* Instructions for setup, running, and testing the application.

9. Deployment

* Provide a Docker Compose file to simplify deployment.

* Ensure all services (e.g., API, Message Broker, InfluxDB) are configured and
can be launched with a single command.


## Deliverables
1. Source code hosted in a Git repository.
2. A clear and concise README.md file.
3. Unit and integration tests demonstrating system reliability.
4. A Docker Compose file to set up and run the system.

## Evaluation Criteria
## Technical Proficiency

* Quality of the architecture and adherence to best practices.


* Proper use of ASP.NET Core and integration with InfluxDB and the Message Broker.

## Performance and Scalability

* Ability to handle high-volume data ingestion and processing.


* Efficiency of data querying and aggregation.

## Code Quality

* Readability and adherence to clean code principles.

## Error Handling and Logging

* Robustness of the error-handling mechanism.


* Quality and usefulness of diagnostic logs.

## Testing

* Coverage and relevance of unit and integration tests.


* Handling of edge cases and system robustness.

![07](https://github.com/user-attachments/assets/f6e43a82-b376-42ea-b8e9-b0b98f269624)

## Swagger

![09](https://github.com/user-attachments/assets/07a1d6c1-9d86-4749-98a5-a2263e3922f7)


