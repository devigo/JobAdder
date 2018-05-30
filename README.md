# JobAdder
### Coding challenge for Developer
#
### For this exercise, we want you to create a web application that will help a recruiter automatically match candidates to open jobs.
#
### Within your UI, (the form the interface takes is open to your interpretation), for each job, display a candidate that is the most-qualified to fill that job.
#
### There are many approaches with how this task could be achieved, but it will be up to you to analyse and determine what you think is a good way to match a candidate to a job and present it to the UI.
#
### API documentation can be found in the following location:
#### https://jobadder1.docs.apiary.io/#
#
### Additional developer comments
#### Used points/values system to set the best candidates for each job
#### It has been defined that up to 10 candidates per job will be retrieved - this can be setup in the app.config from JobAdder.Integrations
#
### Special feature of the system
#### Used memory cache to increase performance and data returns - the time to expire the cache can be setup in the web.config from JobAdder.Web
