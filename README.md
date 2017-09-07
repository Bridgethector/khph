# khph
  #import "TableViewController.h"
  
  #import "AddViewController.h"

  #import <CoreData/CoreData.h>

  @interface TableViewController ()

  @property (strong) NSMutableArray *devices;

  @end

  @implementation TableViewController


  -(NSManagedObjectContext *)managedObjectContext {
    
    NSManagedObjectContext *context = nil;
    
    id delegate = [[UIApplication sharedApplication] delegate];
    
    if ([delegate performSelector:@selector(managedObjectContext)])
    {
        context = [delegate managedObjectContext];
    }
    
    return context;
  }


  -(void)viewDidAppear:(BOOL)animated 
  {
    
    NSManagedObjectContext *managedObjectContext = [self managedObjectContext];
    
    NSFetchRequest *fetchRequest = [[NSFetchRequest alloc] initWithEntityName:@"Device"];
    
    self.devices = [[managedObjectContext executeFetchRequest:fetchRequest error:nil] mutableCopy];
    
    [self.tableView reloadData];
    
  }

  #pragma mark - Table view data source

  - (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView 
  {
    return 1;
  }

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section 
  {
    return self.devices.count;
  }


  - (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath 
  {

    static NSString *CellIndenifier = @"Cell";
    
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIndenifier forIndexPath:indexPath];
    
    NSManagedObjectModel *device = [self.devices objectAtIndex:indexPath.row];
    
    [cell.textLabel setText:[NSString stringWithFormat:@"%@ %@",[device valueForKey:@"text1"], [device valueForKey:@"text2"]]];
    
    [cell.detailTextLabel setText:[device valueForKey:@"text3"]];
    
    return cell;
  }



// Override to support conditional editing of the table view.

  - (BOOL)tableView:(UITableView *)tableView canEditRowAtIndexPath:(NSIndexPath *)indexPath 
  {
    
    return YES;
    
    
  }



  // Override to support editing the table view.
  - (void)tableView:(UITableView *)tableView commitEditingStyle:(UITableViewCellEditingStyle)editingStyle forRowAtIndexPath:(NSIndexPath *)indexPath {

    NSManagedObjectContext *context = [self managedObjectContext];
    
    if (editingStyle == UITableViewCellEditingStyleDelete) 
    {
        [context deleteObject:[self.devices objectAtIndex:indexPath.row]];;
        
        NSError *error = nil;
        
        if (![context save:&error]) 
        {
            NSLog(@"%@ %@", error, [error localizedDescription]);
        }
        
        [self.devices removeObjectAtIndex:indexPath.row];
        
        [self.tableView deleteRowsAtIndexPaths:[NSArray arrayWithObject:indexPath] withRowAnimation:UITableViewRowAnimationFade];
        
        
     }
    
    
    
  }


#pragma mark - Navigation

  // In a storyboard-based application, you will often want to do a little preparation before navigation
  
  - (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender
  {
    
    if ([[segue identifier] isEqualToString:@"UpdateCar"])
    {
        NSManagedObjectModel *SelectedDevice = [self.devices objectAtIndex:[[self.tableView indexPathForSelectedRow] row]];
        
        AddViewController *addView = segue.destinationViewController;
        
        addView.device = SelectedDevice;
    }

    
}

@end


  #import <UIKit/UIKit.h>

  @interface AddViewController : UIViewController

  @property (weak, nonatomic) IBOutlet UITextField *CarMake;
  
  @property (weak, nonatomic) IBOutlet UITextField *CarModel;
  
  @property (weak, nonatomic) IBOutlet UITextField *CarYear;

  @property (strong) NSManagedObjectModel *device;

- (IBAction)DismissKeyboard:(id)sender;


- (IBAction)SaveData:(id)sender;

@end

  #import "AddViewController.h"
  #import <CoreData/CoreData.h>

  @interface AddViewController ()

  @end

  @implementation AddViewController

  @synthesize device;

  -(NSManagedObjectContext *)managedObjectContext {
    
    NSManagedObjectContext *context = nil;
    
    id delegate = [[UIApplication sharedApplication] delegate];
    
    if ([delegate performSelector:@selector(managedObjectContext)])
    {
        context = [delegate managedObjectContext];
    }
    
    return context;
  }

  - (void)viewDidLoad 
  {
    [super viewDidLoad];

    // Do any additional setup after loading the view.
    
    if (self.device) 
    {
        [self.CarMake setText:[self.device valueForKey:@"text1"]];
        
        [self.CarModel setText:[self.device valueForKey:@"text2"]];
        
        [self.CarYear setText:[self.device valueForKey:@"text3"]];
    }
    
    
  }



/*
#pragma mark - Navigation


  - (IBAction)DismissKeyboard:(id)sender 
  {
    [self resignFirstResponder];
 }

- (IBAction)SaveData:(id)sender
  {
    
    NSManagedObjectContext *context = [self managedObjectContext];
    
    if (self.device) 
  {
        [self.device setValue:self.CarMake.text forKey:@"text1"];

        [self.device setValue:self.CarModel.text forKey:@"text2"];
        
        [self.device setValue:self.CarYear.text forKey:@"text3"];
        
    } else 
    {
        
        NSManagedObject *newDevice = [NSEntityDescription insertNewObjectForEntityForName:@"Device" inManagedObjectContext:context];
        
        [newDevice setValue:self.CarMake.text forKey:@"text1"];
        
        [newDevice setValue:self.CarModel.text forKey:@"text2"];
        
        [newDevice setValue:self.CarYear.text forKey:@"text3"];
        
    }
    
    
    NSError *error = nil;
    
    if (![context save:&error]) 
    {
        NSLog(@"%@ %@", error, [error localizedDescription]);
    }
    
    [self.navigationController popViewControllerAnimated:YES];
    
    
  }

@end


  http://api.soundcloud.com/tracks?genres=rock&client_id=YOUR_CLIENT_ID  - SoundCloudId
  
  1. http://stackoverflow.com/questions/35688367/access-soundcloud-charts-with-api -  Access Soundcloud Charts with API
  
  
  /// ------------------ Latest ---------------------------//
  
 1. pretrained model convet into class we can use in our app - coreml(pretrained model into coeml)
2. Make predictions
3. previously we are using api's for speech and face recognisation now coreml will handle that, image recognisation
4. check wwdc videos of coreml
5.inceptionv3 model we can find which object it is ?
6. Core ML is a framework that can be harnessed to integrate machine learning models into your app
7. Another bonus feature about Core ML is that you can use pre-trained data models as long as you convert it into a Core ML model
Note: You will need Xcode 9 beta to follow tutorial. You will also need a device that is running iOS 11 beta in order to test out some of the feature of this tutorial. While Xcode 9 beta supports both Swift 3.2 and 4.0, all the code is written in Swift 4.
8. https://www.appcoda.com/coreml-introduction/ - coreml example

Arkit 
1. ARKit when iOS 11 drops later this year. This includes all iOS devices that are powered by Apple’s A9 or A10 chip. (ARKit runs on the Apple A9 and A10 processors)
2. ARKit since the framework requires a lot of processing power 
3.ARKit support to iOS, Apple has made it possible for millions of people worldwide to experience augmented reality without having to invest in any additional hardware. like 
microsoft halolens
4.  Developers will likely start releasing apps that take advantage of ARKit on the App Store later this year, and if early demos are anything to go by, they are going to be pretty impressive as well.
5. with arkit we have to use scene kit - 3d ,sprite kit -2d,metal kit - 3d
6. we can show the vitual content  for example objects like cats, planes, dogs are not there but we can show them in 3d or 2d with motion also and we can control that objects
7. this arkit is really very useful for especially unity and unreal engine
8.we can show like real things but not real
9. https://www.youtube.com/watch?v=RsgbdiTJYDQ -  10 INSANE THINGS CREATED WITH APPLE AR KIT! iOS 11
10. https://www.youtube.com/watch?v=Rf5ucN8fxYY - iOS 11: 10 More AR Demos! (Augmented Reality) 

  
