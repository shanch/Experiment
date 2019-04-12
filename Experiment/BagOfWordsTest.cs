using Accord.MachineLearning;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    public class BagOfWordsTest
    {
        public void ExecuteTest()
        {
            string[][] words =
            {
                new string[]{ "今日", "は", "いい", "天気", "です" },
                new string[]{ "明日", "も", "いい", "天気", "でしょう"}
            };

            var codebook = new BagOfWords()
            {
                //MaximumOccurance = 1 // the resulting vector will have only 0's and 1's
                MaximumOccurance = int.MaxValue
            };

            // Compute the codebook (note: this would have to be done only for the training set)
            codebook.Learn(words);

            // Now, we can use the learned codebook to extract fixed-length
            // representations of the different texts (paragraphs) above:

            // Extract a feature vector from the text 1:
            double[] bow1 = codebook.Transform(words[0]);

            // Extract a feature vector from the text 2:
            double[] bow2 = codebook.Transform(words[1]);

            // we could also have transformed everything at once, i.e.
            double[][] bow = codebook.Transform(words);


            // Now, since we have finite length representations (both bow1 and bow2 should
            // have the same size), we can pass them to any classifier or machine learning
            // method. For example, we can pass them to a Logistic Regression Classifier to
            // discern between the first and second paragraphs

            // Lets create a Logistic classifier to separate the two paragraphs:
            var learner = new IterativeReweightedLeastSquares<LogisticRegression>()
            {
                Tolerance = 1e-4,  // Let's set some convergence parameters
                Iterations = 100,  // maximum number of iterations to perform
                Regularization = 0
            };

            // Now, we use the learning algorithm to learn the distinction between the two:
            LogisticRegression reg = learner.Learn(new[] { bow1, bow2 }, new[] { false, true });

            // Finally, we can predict using the classifier:
            bool c1 = reg.Decide(bow1); // Should be false
            bool c2 = reg.Decide(bow2); // Should be true

            Console.WriteLine(c1);
            Console.WriteLine(c2);
        }
    }
}
