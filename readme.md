# Scheduler
## Development
Application is written in C# utilizing 4.5 .NET Framework. The application uses an off-the-shelf scheduler application called Quarts.NET. 
 
## Basic Design
The Scheduler is to be a standalone service allowing administrators to schedule jobs. The purpose of the scheduler is to:
1. Define Jobs to be executed.
	a. Definitions are to be hosted by the database. Ideally new jobs can be created without impact on the scheduler.
	b. Jobs are records in the JobTypes table. Each record has the ability to have a one to many relationship with the JOB_TYPE_PARMETERS_LINK table. This table contains a reference to Key/Value records in a table. Essentially a Job can have defined multiple key/value pair definitions not limiting the amount of parameters to pass into the job when scheduling a job.
	c. Based on the job type, the scheduler instantiates the correct job object. The job object can create other jobs to be scheduled. 
	d. Jobs will be programmed generically allowing flexibility to users to enter jobs to run within the database.
2. Ability to schedule runs of jobs as defined in the database.
	a. Definitions of job runs are stored in the database. The information references defined jobs allowing the user to define cron based triggers. Refer to CronTrigger for further information on how to configure the schedule.
	b. The definition allows the user to define the job's ability to run asynchronously or chained. A chained run means the parent must run first and upon completion, the current job definition will run. 
	c. The definition will also have a field allowing the user to configure how to handle misfires. This is when the scheduler crashes, stops, or any other circumstance preventing a job to run. Refer to CronTrigger at the "CronTrigger Misfire Instructions" section for more information.
3. Keep a history of job run by the scheduler.
	a. There will be a Job History table in the database.
4. The scheduler is a service with a command prompt interface allowing users to execute commands such as running a job immediately.