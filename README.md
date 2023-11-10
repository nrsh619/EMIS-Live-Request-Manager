# EMIS-Live-Request-Manager
Demo project for AWS Hands- on

## Prerequisites

### Configure AWS

To configure Aws update below values in both appSettings.json and appsettings.Development.json

```shell
"AwsConfiguration": {
    "BucketName": "", // name of the s3 bucket 
    "Region": "", // region where the s3 bucket is located
    "AwsAccessKey": "", // accesskey of the user who have required permissions to access the s3 bucket
    "AwsSecretAccessKey": ""  // Secretkey of the user who have required permissions to access the s3 bucket
  }
```

Note: AccessKey and SecretAccessKey  can be found in the .csv file which we downloaded during IAM user creation flow.