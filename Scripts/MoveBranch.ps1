param(
    [string]$originalBranchName
)

# Check out the original branch
git checkout $originalBranchName

# Rename the branch locally
$movedBranchName = "old/frontend/" + $originalBranchName
git branch -m $originalBranchName $movedBranchName

# Push the new branch name to the remote
git push origin $movedBranchName

# Delete the original branch from the remote
git push origin --delete $originalBranchName