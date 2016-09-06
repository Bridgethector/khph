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
